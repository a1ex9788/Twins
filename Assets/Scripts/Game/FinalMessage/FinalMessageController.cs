using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class FinalMessageController : MonoBehaviour
{

    [SerializeField] Text pointsTextAnim;

    [Tooltip("Escala del tiempo del reloj")]
    [Range(-10.0f, 10.0f)]
    public float escalaDelTiempo = 1;

    [SerializeField] GameObject grid;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject finalResults;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resetButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button continueButton;
    [SerializeField] Text messageText;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    [SerializeField] Text scoreTextInFinalMessage;
    [SerializeField] Text failuresText;
    [SerializeField] Text timeText;
    [SerializeField] ScoreScript scoreScript;
    [SerializeField] GameObject referecedCard;
    [SerializeField] GameObject imageBackground;
    [SerializeField] GameObject timePanel;
    [SerializeField] GameDurationTime gameDurationTime;


    AudioSource audioSource;
    int score;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }


    public void ShowWinMenu()
    {
        Destroy(GameObject.Find("musicGame"));
        if (!AudioManager.getIsMutedEffect()) {
            gameManager.PlayMenusMusic();
            audioSource.PlayOneShot(winSound);
        }    

        if (gameManager.IsLevelMode())
        {
            int currentLevel = gameManager.GetCurrentLevel();
            messageText.text = "¡¡ENHORABUENA, HAS SUPERADO EL NIVEL " + currentLevel + "!!";
            if (currentLevel < 10) { PersistenceManager.SaveNewUnlockedLevel(currentLevel + 1); }
        }

        if (gameManager.IsFreeMode()) { messageText.text = "¡¡ENHORABUENA HAS GANADO!!"; }

        if (gameManager.IsLimitTime())
        {
            float gameTime = gameManager.GetLimitTime() - gameManager.GetRemainingTime();
            float minutes = Mathf.FloorToInt(gameTime / 60);
            float seconds = Mathf.FloorToInt(gameTime % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            StartCoroutine(Aumento(scoreScript.getScore()));
        }
        else
        {
            gameDurationTime.Pause();
        }

        if (gameManager.IsByCardGame()) { referecedCard.SetActive(false); }

        ShowButtonsAndMenu();

        pointsTextAnim.text = "";
    }

    public void ShowLoseMenu()
    {
        Destroy(GameObject.Find("musicGame"));
        if (!AudioManager.getIsMutedEffect()) { gameManager.PlayMenusMusic(); }
        messageText.text = "LO SENTIMOS!!! HAS PERDIDO";
        if (!AudioManager.getIsMutedEffect()) { audioSource.PlayOneShot(loseSound); }
        float gameTime = gameManager.GetLimitTime();
        float minutes = Mathf.FloorToInt(gameTime / 60);
        float seconds = Mathf.FloorToInt(gameTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (gameManager.IsByCardGame()) { referecedCard.SetActive(false); }

        ShowButtonsAndMenu();
        pointsTextAnim.text = "";

    }

    public void ShowWinMenuChallenge()
    {


        PersistenceManager.RaiseTotalMistakeChallenge(gameManager.GetNumFailures());
        PersistenceManager.RaiseTotalTimeChallenge(gameManager.GetLimitTime() - gameManager.GetRemainingTime());
        PersistenceManager.RaiseChallengeCurrentScore(scoreScript.getScore());

        if (GameConfiguration.GetGameConfiguration().currentLevel < 5)
        {
            StartCoroutine(BetweenLevelsInChallengeMode());          
        }
        else
        {
            Destroy(GameObject.Find("musicGame"));
            if (!AudioManager.getIsMutedEffect())
            {
                gameManager.PlayMenusMusic();
                audioSource.PlayOneShot(winSound);
            }

            //Tiempo total desafío
            float gameTime = PersistenceManager.totalTimeChallenge;
            float minutes = Mathf.FloorToInt(gameTime / 60);
            float seconds = Mathf.FloorToInt(gameTime % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            messageText.text = "¡¡ENHORABUENA, HAS SUPERADO EL DESAFÍO!!";
            ShowButtonsAndMenu();
            failuresText.text = PersistenceManager.totalMistakesChallenge.ToString();
            resetButton.gameObject.SetActive(false);

            scoreTextInFinalMessage.text = PersistenceManager.currentChallengeScore.ToString();
            if (PersistenceManager.currentChallengeScore > PersistenceManager.GetChallengeMaxScore(GameConfiguration.currentChallenge - 1))
            {
                PersistenceManager.SaveChallengeMaxScore(GameConfiguration.currentChallenge - 1);
            }  
        }

        pointsTextAnim.text = "";
    }
    IEnumerator BetweenLevelsInChallengeMode()
    {
        yield return new WaitForSeconds(1);
        NextChallengeLevel();
    }

    void ShowButtonsAndMenu()
    {
        imageBackground.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        scoreScript.activeDesactive(false);
        scoreTextInFinalMessage.text = scoreScript.gameScore();
        failuresText.text = gameManager.GetNumFailures().ToString();
        grid.SetActive(false);
        finalResults.SetActive(true);
        gameManager.PauseTurnTime();
        gameManager.PauseGameTime();
        timePanel.SetActive(false);

    }


    public void ExitGameEnd()
    {
        if (gameManager.IsFreeMode()) { SceneManager.LoadScene("StartMenu"); }
        if (gameManager.IsLevelMode()) { SceneManager.LoadScene("LevelMode"); }
        if (gameManager.IsChallengeMode()) { SceneManager.LoadScene("ChallengeMode"); }
    }


    public void RestartGameEnd()
    {
        if (gameManager.IsChallengeMode())
        {
            GameConfiguration.SetChallengeGameConfiguration(GameConfiguration.currentChallenge, 1);
            PersistenceManager.currentChallengeScore = 0;
        }
        SceneManager.LoadScene("Game");
    }

    public double CalculateBonusScore()
    {
        float extraTime = gameManager.GetRemainingTime() / gameManager.GetLimitTime();
        float extraTimeAndCouples = (extraTime * (float)gameManager.GetNumCouples());
        return Mathf.RoundToInt(extraTimeAndCouples) * 5;
    }



    IEnumerator Aumento(int score) {

        resetButton.interactable = false;
        exitButton.interactable = false;

        scoreTextInFinalMessage.text = score.ToString();
        yield return new WaitForSeconds(1);

        int bonusScorePlusScore = score + (int) CalculateBonusScore();
        
        while ( score <= bonusScorePlusScore) {

            scoreTextInFinalMessage.text = score.ToString();
            score += 1;
            yield return new WaitForSeconds(.07f);

        }

        resetButton.interactable = true;
        exitButton.interactable = true;

    }



    public void NextChallengeLevel() {
        GameConfiguration.SetChallengeGameConfiguration(GameConfiguration.currentChallenge, GameConfiguration.GetGameConfiguration().currentLevel + 1);
        SceneManager.LoadScene("Game");
    }
}