using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGameInitializer : GameInitializer
{
    public LevelGameInitializer(FlippingControl flippingControl, CardCreation cardCreation, GameObject timePanel, GameObject gameTimeController, GameObject gameLabel, GameDurationTime gameDurationTime, GameConfiguration gameConfiguration, ScoreScript scoreScript, Button pauseButton)
        : base(flippingControl, cardCreation, timePanel, gameTimeController, gameLabel, gameDurationTime, gameConfiguration, scoreScript, pauseButton)
    {
    }

    protected override void FlippingControlManagement()
    {
        GameObject gameObject = FindObjectOfType<GameManager>().gameObject;
        flippingControl = gameObject.AddComponent<FlippingControl>();
    }

    protected override void ScoreManagement()
    {
        scoreScript.resetScore();
    }
}
