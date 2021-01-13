using GracesGames.SimpleFileBrowser.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DecksEditor : MonoBehaviour
{

    [SerializeField] Dropdown decksDropdown;
    [SerializeField] GameObject NewDeckPanel;
    [SerializeField] InputField newDeckName;
    [SerializeField] GameObject errorText;

    [SerializeField] DemoCaller fileBrowser;

    [SerializeField] GameObject backSideParent;
    [SerializeField] GameObject cardsParent;
    [SerializeField] GameObject cardPrefab;

    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;

    bool fileBrowserOpenForBackSide;
    public bool deleteImage = false;

    void Awake()
    {
        foreach (string deckName in ImagesPersistence.GetDecksNames()) {
            decksDropdown.options.Add(new Dropdown.OptionData(deckName));
        }


        decksDropdown.onValueChanged.AddListener(delegate {
            CleanBackSide();
            CleanCards();

            LoadBackSideImage(decksDropdown.options[decksDropdown.value].text);
            LoadCardsImages(decksDropdown.options[decksDropdown.value].text);

            button1.interactable = true;
            button2.interactable = true;
            button3.interactable = true;
        });

        decksDropdown.value = 1;
        decksDropdown.value = 0;
    }

    void LoadBackSideImage(string deckName) {
        CleanBackSide();

        GameObject card = Instantiate(cardPrefab, new Vector3(-5.71f, -0.89f), Quaternion.Euler(0, 0, 0), backSideParent.transform);
        card.GetComponent<Image>().sprite = ImagesPersistence.LoadBackSide(deckName);
    }

    void LoadCardsImages(string deckName) {
        CleanCards();

        List<Sprite> images = ImagesPersistence.LoadCardsImages(deckName);
        foreach (Sprite s in images) {
            GameObject card = Instantiate(cardPrefab, new Vector3(0, -0.9f), Quaternion.Euler(0, 0, 0), cardsParent.transform);
            card.GetComponent<Image>().sprite = s;
        }
    }

    void CleanBackSide() {
        for (int i = 0; i < backSideParent.transform.childCount; i++) {
            Destroy(backSideParent.transform.GetChild(0).gameObject);
        }
    }

    void CleanCards() {
        for (int i = 0; i < cardsParent.GetComponentsInChildren<Image>().Length; i++) {
            Destroy(cardsParent.GetComponentsInChildren<Image>()[i].gameObject);
        }
    }

    public void GoStartMenu() {
        SceneManager.LoadScene("StartMenu");
    }

    public void NewDeck() {
        newDeckName.text = "";
        NewDeckPanel.SetActive(true);
        errorText.SetActive(false);
    }

    public void DontSaveNewDeck() {
        NewDeckPanel.SetActive(false);
    }

    public void SaveNewDeck() {
        if (newDeckName.text.Equals("")) { errorText.SetActive(true); }
        else {
            NewDeckPanel.SetActive(false);

            decksDropdown.options.Add(new Dropdown.OptionData(newDeckName.text));
            decksDropdown.value = decksDropdown.options.Count;
        }
    }

    public void LoadBackSide() {
        OpenFileBrowser();
        fileBrowserOpenForBackSide = true;
    }

    public void LoadImage() {
        OpenFileBrowser();
        fileBrowserOpenForBackSide = false;
    }

    public void DeleteImage() {
        deleteImage = true;
    }

    public string GetCurrentDeckName() {
        return decksDropdown.options[decksDropdown.value].text;
    }

    void OpenFileBrowser()
    {
        fileBrowser.OpenFileBrowser(true);
    }

    public void SetImageLoaded(string path) {
        if (fileBrowserOpenForBackSide) {
            ImagesPersistence.SaveBackSide(path, decksDropdown.options[decksDropdown.value].text);
            LoadBackSideImage(decksDropdown.options[decksDropdown.value].text);
        } else {
            ImagesPersistence.SaveCardImage(path, decksDropdown.options[decksDropdown.value].text);
            LoadCardsImages(decksDropdown.options[decksDropdown.value].text);
        }
    }


    public void DeleteDeck() {
        if (decksDropdown.options.Count > 0) {
            string deckName = decksDropdown.options[decksDropdown.value].text;

            decksDropdown.options.Remove(decksDropdown.options[decksDropdown.value]);

            ImagesPersistence.DeleteDeck(deckName);

            decksDropdown.GetComponentInChildren<Text>().text = "";

            CleanBackSide();
            CleanCards();

            if (decksDropdown.options.Count == 0) decksDropdown.GetComponentInChildren<Text>().text = "";
        }
    }
}
