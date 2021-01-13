using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ByCardGameInitializer : GameInitializer
{
    GameObject referecedCard;
    TurnTimeController turnTimeController;

    public ByCardGameInitializer(FlippingControl flippingControl, CardCreation cardCreation, GameObject timePanel, GameObject gameTimeController, GameObject gameLabel, GameDurationTime gameDurationTime, GameConfiguration gameConfiguration, ScoreScript scoreScript, Button pauseButton, GameObject referecedCard, TurnTimeController turnTimeController)
        : base(flippingControl, cardCreation, timePanel, gameTimeController, gameLabel, gameDurationTime, gameConfiguration, scoreScript, pauseButton)
    {
        this.referecedCard = referecedCard;
        this.turnTimeController = turnTimeController;
    }

    protected override void FlippingControlManagement()
    {
        GameObject gameObject = FindObjectOfType<GameManager>().gameObject;
        flippingControl = gameObject.AddComponent<ByCardGameFlippingControl>();
        referecedCard.SetActive(true);
    }

    protected override void ShowTimeManagement()
    {
        if (gameConfiguration.HasShowTime())
        {
            UnCoverAll();

            pauseButton.interactable = false;
            StartCoroutine(ActivePauseButtonAfter());

            turnTimeController.Pause();
            gameTimeController.GetComponent<GameTimeController>().Pause();

            turnTimeController.ResetTurnAfter(gameConfiguration.GetShowTime());
            if (gameConfiguration.HasLimitTime()) gameTimeController.GetComponent<GameTimeController>().RestartGameAfter(gameConfiguration.GetShowTime());
        }
    }

    protected override void ScoreManagement()
    {
        scoreScript.resetScore();
    }
}
