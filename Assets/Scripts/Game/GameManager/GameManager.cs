using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour

{

    [SerializeField] ScoreScript scoreScript;    
    [SerializeField] TurnTimeController turnTimeController;
    FlippingControl flippingControl;
    BackgroundMusicController backgroundMusic;

    [SerializeField] FinalMessageController finalMessageController;
    [SerializeField] CardCreation cardCreation;
    [SerializeField] GameObject timePanel;
    [SerializeField] GameObject gameTimeController;
    [SerializeField] GameObject gameLabel;
    [SerializeField] GameObject referecedCard;
    [SerializeField] Button pauseButton;
    [SerializeField] GameDurationTime gameDurationTime;
    GameConfiguration gameConfiguration;
    [SerializeField] Text scoreTextInGame; 

    [SerializeField] GameObject backgroundImage;
    [SerializeField] GameObject backgroundImage2;
    [SerializeField] GameObject backgroundImage3;
    [SerializeField] GameObject backgroundImage4;


    private void Awake()
    {
        gameConfiguration = GameConfiguration.GetGameConfiguration();
        backgroundMusic = FindObjectOfType<BackgroundMusicController>();
        StartGame();
    }

    public void StartGame()
    {
        GameInitializer gameInitializer = null;

        switch (gameConfiguration.gameMode) {
            case GameMode.standardGame:
                gameInitializer = new StandarGameInitializer(flippingControl,cardCreation,timePanel,gameTimeController,gameLabel,gameDurationTime,gameConfiguration,scoreScript,pauseButton,turnTimeController);
                break;
            case GameMode.byCardGame:
                gameInitializer = new ByCardGameInitializer(flippingControl, cardCreation, timePanel, gameTimeController, gameLabel, gameDurationTime, gameConfiguration, scoreScript, pauseButton, referecedCard, turnTimeController);
                break;
            case GameMode.levelMode:
                gameInitializer = new LevelGameInitializer(flippingControl, cardCreation, timePanel, gameTimeController, gameLabel, gameDurationTime, gameConfiguration, scoreScript, pauseButton);
                break;
            case GameMode.challengeMode:
                gameInitializer = new ChallengeGameInitializer(flippingControl, cardCreation, timePanel, gameTimeController, gameLabel, gameDurationTime, gameConfiguration, scoreScript, pauseButton, scoreTextInGame, backgroundImage, backgroundImage2, backgroundImage3, backgroundImage4);
                break;
        }

        flippingControl = gameInitializer.InitializeGame();
    }

    public int GetRows() { return gameConfiguration.GetRows(); }
    public int GetColumns() { return gameConfiguration.GetColumns(); }
    public int GetNumCouples() { return gameConfiguration.GetNumCouples(); }
    public float GetExposingTime() { return gameConfiguration.GetExposingTime(); }
    public float GetLimitTime() { return gameConfiguration.GetLimitTime(); }
    public float GetTurnTime() { return gameConfiguration.GetTurnTime(); }
    public bool IsLimitTime() { return gameConfiguration.HasLimitTime(); }
    public int GetCurrentLevel() { return gameConfiguration.GetCurrentLevel(); }
    public bool IsShowCards() { return gameConfiguration.HasShowTime(); }
    public bool IsFreeMode() { return gameConfiguration.IsFreeMode(); }
    public bool IsByCardGame() { return gameConfiguration.IsByCardGame(); }
    public bool IsByCategoryGame() { return gameConfiguration.IsByCategoryGame(); }
    public bool IsLevelMode() { return gameConfiguration.IsLevelMode(); }
    public bool IsChallengeMode() { return gameConfiguration.IsChallengeMode(); }



    void UnCoverAll() {
        foreach (CardFlipping card in GetCardList())
        {
            card.Uncover();
            card.CoverAfter(gameConfiguration.GetShowTime());
        }
    }


    IEnumerator ActivePauseButtonAfter()
    {
        yield return new WaitForSeconds(gameConfiguration.GetShowTime());
        pauseButton.interactable = true;
    }





    public void DecreaseScore(CardFlipping cardOne, CardFlipping cardTwo)
    {
        scoreScript.DecreaseScore(cardOne, cardTwo);
    }

    public void ResetTurnAfter(float v)
    {
        turnTimeController.ResetTurnAfter(v);
    }

    public void PauseTurnTime()
    {
        turnTimeController.Pause();
    }
    
    public void PauseGameTime()
    {
        gameTimeController.GetComponent<GameTimeController>().Pause();
    }

    public void RestartTurnTime()
    {
        turnTimeController.Restart();
    }

    public void RestartGameTime()
    {
        gameTimeController.GetComponent<GameTimeController>().Restart();
    }

    public FlashingText GetTurnTimeFlashingText() {
        return turnTimeController.GetComponent<FlashingText>();
    }

    public FlashingText GetGameTimeFlashingText()
    {
        return gameTimeController.GetComponent<FlashingText>();
    }
    public float GetRemainingTime() {
        return gameTimeController.GetComponent<GameTimeController>().GetRemainingTime();
    }

    public void ShowWinMenu() {
        finalMessageController.ShowWinMenu();
    }
    public void ShowWinMenuChallenge()
    {
        finalMessageController.ShowWinMenuChallenge();
    }
    public void ShowLoseMenu() {
        finalMessageController.ShowLoseMenu();
    }

    public List<CardFlipping> GetCardList() {
        return cardCreation.GetCardList();
    }

    public void IncreaseScore()
    {
        scoreScript.IncreaseScore();
    }

    public void ResetTurn() {
        turnTimeController.ResetTurn();
    }

    public int GetNumFailures() {
        return flippingControl.GetNumFailures();
    }

    public void TurnPenalty() {
        scoreScript.TakeTurnPenalty();
    }

    public void KillTurnTimeCorutine() {
        turnTimeController.KillTurnTimeCorutine();
    }

    public void PlayMenusMusic() {
        backgroundMusic.PlayMenusMusic();
    }

    public void CoverFirstCard() {
        flippingControl.CoverFirstCard();
    }

}
