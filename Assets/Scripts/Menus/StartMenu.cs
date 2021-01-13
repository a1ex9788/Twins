using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    [SerializeField] Image imageFade;
    [SerializeField] GameObject yesNoDialog;

    [SerializeField] Text errorText;

 
    public void StartStandardGame()
    {
        GameConfiguration.SetCustomGameConfiguration(GameMode.standardGame);
        if (DeckIsOK())
        {            
            SceneManager.LoadScene("Game");
        }
        else
        {
            errorText.text = "La baraja seleccionada no tiene suficientes cartas para jugar con el tablero seleccionado";
        }
    }

    public void StartByCardGame()
    {
        GameConfiguration.SetCustomGameConfiguration(GameMode.byCardGame);
        if (DeckIsOK())
        {            
            SceneManager.LoadScene("Game");
        }
        else
        {
            errorText.text = "La baraja seleccionada no tiene suficientes cartas para jugar con el tablero seleccionado";
        }
    }

    public void StartByCategoryGame()
    {
        GameConfiguration.SetCustomGameConfiguration(GameMode.byCategoryGame);
        if (DeckIsOK())
        {            
            SceneManager.LoadScene("Game");
        }
        else {
            errorText.text = "La baraja seleccionada no tiene suficientes cartas para jugar con el tablero seleccionado";
        }
    }

    bool DeckIsOK() {
        return ImagesLoader.HowManyCards(GameConfiguration.GetGameConfiguration().deckName) * 2 >= GameConfiguration.GetGameConfiguration().columns * GameConfiguration.GetGameConfiguration().rows;
    }

    public void StartLevelMode()
    {
        SceneManager.LoadScene("LevelMode");
    }

    public void StartChallengeMode()
    {
        SceneManager.LoadScene("ChallengeMode");
    }

    public void StartTournamentMode()
    {
        SceneManager.LoadScene("TournamentMode");
    }

    public void GoConfigurationMenu()
    {
        SceneManager.LoadScene("ConfigurationMenu");
    }

    public void ExitGame()
    {
        yesNoDialog.SetActive(true);
    }

    public void GoDecksEditor()
    {
        SceneManager.LoadScene("DecksEditor");
    }

    public void onUserClickYesNo(int choice)
    {
        if (choice == 1)
        {
            Application.Quit();
        }

        yesNoDialog.SetActive(false);
    }

}
