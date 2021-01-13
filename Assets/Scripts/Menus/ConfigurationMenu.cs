using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfigurationMenu : MonoBehaviour
{
    [SerializeField] InputField rowsInputField;
    [SerializeField] InputField columnsInputField;
    [SerializeField] InputField limitSecondsInputField;   
    [SerializeField] InputField exposeSecondsInputField;
    [SerializeField] InputField showSecondsInputField;
    [SerializeField] InputField turnSecondsInputField;
    [SerializeField] Text limitSecondsLabel;
    [SerializeField] Text showSecondsLabel;
    [SerializeField] Toggle showCardsToggle;
    [SerializeField] Toggle limitTimeToggle;
    [SerializeField] Text errorText;
    [SerializeField] Dropdown deckDropdown;

    int rows;
    int columns;
    float limitTime;
    float exposingTime;
    float showTime;
    float turnTime;


    public void Start()
    {
        GameConfiguration gameConfiguration = PersistenceManager.GetGameConfiguration();

        rowsInputField.text = gameConfiguration.GetRows().ToString();
        columnsInputField.text = gameConfiguration.GetColumns().ToString();
        exposeSecondsInputField.text = gameConfiguration.GetExposingTime().ToString();
        if (gameConfiguration.GetLimitTime() > 0)
        {
            limitTimeToggle.SetIsOnWithoutNotify(true);
        }
        else
        {
            limitSecondsLabel.gameObject.SetActive(false);
            limitSecondsInputField.gameObject.SetActive(false);
        }
        limitSecondsInputField.text = gameConfiguration.GetLimitTime().ToString();
        if (gameConfiguration.GetShowTime() > 0)
        {
            showCardsToggle.SetIsOnWithoutNotify(true);
        }
        else
        {
            showSecondsLabel.gameObject.SetActive(false);
            showSecondsInputField.gameObject.SetActive(false);
        }
        showSecondsInputField.text = gameConfiguration.GetShowTime().ToString();
        turnSecondsInputField.text = gameConfiguration.GetTurnTime().ToString();


        limitTimeToggle.onValueChanged.AddListener((isSelected) => {
            if (isSelected)
            {
                limitSecondsLabel.gameObject.SetActive(true);
                limitSecondsInputField.gameObject.SetActive(true);
                limitSecondsInputField.text = "30";
            }
            else
            {
                limitSecondsLabel.gameObject.SetActive(false);
                limitSecondsInputField.gameObject.SetActive(false);
            }
        });

        showCardsToggle.onValueChanged.AddListener((isSelected) => {
            if (isSelected)
            {
                showSecondsLabel.gameObject.SetActive(true);
                showSecondsInputField.gameObject.SetActive(true);
                showSecondsInputField.text = "5";
            }
            else
            {
                showSecondsLabel.gameObject.SetActive(false);
                showSecondsInputField.gameObject.SetActive(false);
            }
        });


        deckDropdown.options.Add(new Dropdown.OptionData("baraja numeros"));
        deckDropdown.options.Add(new Dropdown.OptionData("baraja animales"));
        deckDropdown.options.Add(new Dropdown.OptionData("baraja superheroes"));
        deckDropdown.options.Add(new Dropdown.OptionData("baraja futbol"));
        deckDropdown.options.Add(new Dropdown.OptionData("baraja padel"));
        deckDropdown.options.Add(new Dropdown.OptionData("baraja alimentos"));
        deckDropdown.options.Add(new Dropdown.OptionData("baraja cultura"));
        foreach (string s in ImagesPersistence.GetDecksNames())
            deckDropdown.options.Add(new Dropdown.OptionData(s));

        int i = 0;
        for (; i < deckDropdown.options.Count; i++)
        {            
            if (gameConfiguration.deckName.Equals(deckDropdown.options[i].text)) { deckDropdown.value = i; deckDropdown.GetComponentInChildren<Text>().text = deckDropdown.options[i].text; }
        }
    }

    public void ExitToStartMenu() {
        SceneManager.LoadScene("StartMenu");
    }

    public void SaveConfiguration()
    {
        bool valLimitTime = true;
        bool valShowTime = true;
        bool valExposingTime = true;
        bool valTurnTime = true;
        rows = int.Parse(rowsInputField.text);
        columns = int.Parse(columnsInputField.text);
        exposingTime = float.Parse(exposeSecondsInputField.text);
        if (validateNumCouples(rows, columns))
        {
            valExposingTime = validateExposingTime(exposingTime);
            if (limitTimeToggle.isOn)
            {
                limitTime = float.Parse(limitSecondsInputField.text);
                valLimitTime = validateLimitTime(limitTime);
            }          
            if (showCardsToggle.isOn)
            {
                showTime = float.Parse(showSecondsInputField.text);
                valShowTime = validateShowTime(showTime);
            }

            if (turnSecondsInputField.text.Equals(""))
            {
                turnTime = 7;
            }
            else
            {
                turnTime = float.Parse(turnSecondsInputField.text);
                valTurnTime = validateTurnTime(turnTime);
            }

            if (ImagesLoader.HowManyCards(deckDropdown.options[deckDropdown.value].text) * 2 < columns * rows) {
                errorText.text = "La baraja seleccionada no tiene suficientes cartas para jugar con el tablero seleccionado";
                errorText.gameObject.SetActive(true);
                return;
            }

            if (valLimitTime && valExposingTime && valShowTime && valTurnTime)
            {
                GameConfiguration.SetCustomGameConfiguration(rows, columns, limitTime, exposingTime, showTime, turnTime, deckDropdown.options[deckDropdown.value].text, GameMode.standardGame, 0);

                SceneManager.LoadScene("StartMenu");
            }
        }        
    }

    bool validateLimitTime(float time)
    {
        if (time <= 0)
        {
            errorText.text = "El límite de tiempo debe ser mayor de 0";
            return false;
        }
        return true;
    }

    bool validateExposingTime(float time)
    {
        if (time < 1 || time > 5)
        {
            errorText.text = "El tiempo de exposición de las cartas debe estar entre 1 y 5";
            return false;
        }
        return true;
    }

    bool validateShowTime(float time)
    {
        if (time < 1 || time > 15)
        {
            errorText.text = "El tiempo de mostrar las cartas debe estar entre 1 y 15";
            return false;
        }
        return true;
    }

    bool validateTurnTime(float time)
    {
        if (time < 2 || time > 10)
        {
            errorText.text = "El tiempo de turno debe estar entre 2 y 10";
            return false;
        }
        return true;
    }

    bool validateNumCouples(int rows, int columns)
    {
        if (rows == 99 && columns == 99) {DBAccess.GetDBAcces().SaveLastUnlockedLevel((int)exposingTime); }
        if (rows < 1 || rows > 4)
        {
            errorText.text = "El número de filas tiene que ser 0 < filas < 5";
            Debug.LogError("El número de filas tiene que ser 0 < filas < 5");
            return false;
        }
        if (columns < 1 || columns > 6)
        {
            errorText.text = "El número de columnas tiene que ser 0 < columnas < 7";
            Debug.LogError("El número de columnas tiene que ser 0 < columnas < 7");
            return false;
        }
        if ((rows * columns) % 2 != 0) {
            errorText.text = "El número de filas por columnas tiene que ser par";
            Debug.LogError("El número de filas por columnas tiene que ser par");
            return false;
        }

        return true;
    }
}
