using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button PauseButton;
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Animator pauseMenuAnim;
    [SerializeField] private Animator timeAnim;
    [SerializeField] private GameManager gameManager;
    [SerializeField] Text scoreTextInPause;
    [SerializeField] ScoreScript scoreScript;
    [SerializeField] AudioClip openPauseSound;
    [SerializeField] GameObject yesNoDialog;
    [SerializeField] GameObject yesButton;
    [SerializeField] GameObject noButton;
    [SerializeField] GameObject timePanel;
    [SerializeField] GameObject yesNoDialogRestart;
    [SerializeField] GameObject referecedCard;

    [SerializeField] private TurnTimeController turnTimeController;
    [SerializeField] private GameTimeController gameTimeController;

    AudioSource audioSource;

    AudioSource backgroundMusicController;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        backgroundMusicController = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();

    }
    public void OpenPauseMenuPanel()
    {
        backgroundMusicController.Stop();

        if (!AudioManager.getIsMutedEffect()) { audioSource.PlayOneShot(openPauseSound); }
        grid.SetActive(false);
        scoreScript.activeDesactive(false);
        PauseButton.gameObject.SetActive(false);
        referecedCard.SetActive(false);
        pauseMenu.SetActive(true);
        pauseMenuAnim.Play("SlideIn");
        timeAnim.Play("GoMiddle");
        scoreTextInPause.text = scoreScript.gameScore();
        turnTimeController.GetComponent<FlashingText>().enabled = true;
        gameTimeController.GetComponent<FlashingText>().enabled = true;

        turnTimeController.Pause();
        gameTimeController.Pause();
    }


    public void ClosePauseMenuPanel()
    {
        backgroundMusicController.Play();
        if (!AudioManager.getIsMutedEffect()) { audioSource.PlayOneShot(openPauseSound); }
        StartCoroutine(ClosePauseMenu());
        turnTimeController.GetComponent<FlashingText>().enabled = false;
        gameTimeController.GetComponent<FlashingText>().enabled = false;

    }

    public void onUserClickYesNo(int choice)
    {
        if (choice == 1)
        {
            if (gameManager.IsFreeMode()) { SceneManager.LoadScene("StartMenu"); }
            if (gameManager.IsLevelMode()) { SceneManager.LoadScene("LevelMode"); }
            if (gameManager.IsChallengeMode()) { SceneManager.LoadScene("ChallengeMode"); }
        }

        if (choice == 0)
        {
            yesNoDialog.SetActive(false);
        }
    }

    public void onUserClickYesNoRestart(int choice)
    {
        if (choice == 1)
        {
            SceneManager.LoadScene("Game");
        }

        if (choice == 0)
        {
            yesNoDialogRestart.SetActive(false);
        }
    }

    public void ExitGame()
    {
        yesNoDialog.SetActive(true);

    }

    IEnumerator ClosePauseMenu()
    {
        pauseMenuAnim.Play("SlideOut");
        timeAnim.Play("GoHome");
        turnTimeController.GetComponent<Text>().enabled = true;
        gameTimeController.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(1f);
        turnTimeController.Restart();
        gameTimeController.Restart();


        pauseMenu.SetActive(false);
        scoreScript.activeDesactive(true);
        grid.SetActive(true);
        PauseButton.gameObject.SetActive(true);
        if (gameManager.IsByCardGame())  referecedCard.SetActive(true);
    }

    public void RestartGame()
    {
        yesNoDialogRestart.SetActive(true);
    }
}

