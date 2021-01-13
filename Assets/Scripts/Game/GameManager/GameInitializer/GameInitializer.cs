using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameInitializer : MonoBehaviour
{
    protected FlippingControl flippingControl;
    protected CardCreation cardCreation;
    protected GameObject timePanel;
    protected GameObject gameTimeController;
    protected GameObject gameLabel;
    protected GameDurationTime gameDurationTime;
    protected GameConfiguration gameConfiguration;
    protected ScoreScript scoreScript;
    protected Button pauseButton;

    protected GameInitializer(FlippingControl flippingControl, CardCreation cardCreation, GameObject timePanel, GameObject gameTimeController, GameObject gameLabel, GameDurationTime gameDurationTime, GameConfiguration gameConfiguration, ScoreScript scoreScript, Button pauseButton)
    {
        this.flippingControl = flippingControl;
        this.cardCreation = cardCreation;
        this.timePanel = timePanel;
        this.gameTimeController = gameTimeController;
        this.gameLabel = gameLabel;
        this.gameDurationTime = gameDurationTime;
        this.gameConfiguration = gameConfiguration;
        this.scoreScript = scoreScript;
        this.pauseButton = pauseButton;
    }

    protected virtual void BackGroundImageManagement() { }
    public void LimitTimeManagement() {
        if (!gameConfiguration.HasLimitTime())
        {
            gameTimeController.SetActive(false);
            gameLabel.SetActive(false);
            gameDurationTime.StartGameDuration();
        }
    }
    protected abstract void FlippingControlManagement();
    protected void CreateCards() {
        cardCreation.CreateCards(
            gameConfiguration.GetRows(),
            gameConfiguration.GetColumns(),
            ImagesLoader.LoadDeck(gameConfiguration.GetDeckName()));
    }
    protected void TimeManagement() { timePanel.SetActive(true); }
    protected virtual void ShowTimeManagement() { }
    protected void RunFlippingControl() { flippingControl.Run(); }
    protected abstract void ScoreManagement();


    public FlippingControl InitializeGame() {
        BackGroundImageManagement();
        LimitTimeManagement();
        FlippingControlManagement();
        CreateCards();
        TimeManagement();
        ShowTimeManagement();
        RunFlippingControl();
        ScoreManagement();

        return flippingControl;
    }



    protected IEnumerator ActivePauseButtonAfter()
    {
        yield return new WaitForSeconds(gameConfiguration.GetShowTime());
        pauseButton.interactable = true;
    }

    protected void UnCoverAll()
    {
        foreach (CardFlipping card in cardCreation.GetCardList())
        {
            card.Uncover();
            card.CoverAfter(gameConfiguration.GetShowTime());
        }
    }
}
