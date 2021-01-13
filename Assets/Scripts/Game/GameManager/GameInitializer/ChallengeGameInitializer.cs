using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeGameInitializer : GameInitializer
{
    Text scoreTextInGame;
    GameObject backgroundImage;
    GameObject backgroundImage2;
    GameObject backgroundImage3;
    GameObject backgroundImage4;

    public ChallengeGameInitializer(FlippingControl flippingControl, CardCreation cardCreation, GameObject timePanel, GameObject gameTimeController, GameObject gameLabel, GameDurationTime gameDurationTime, GameConfiguration gameConfiguration, ScoreScript scoreScript, Button pauseButton, Text scoreTextInGame, GameObject backgroundImage, GameObject backgroundImage2, GameObject backgroundImage3, GameObject backgroundImage4)
        : base(flippingControl, cardCreation, timePanel, gameTimeController, gameLabel, gameDurationTime, gameConfiguration, scoreScript, pauseButton)
    {
        this.scoreTextInGame = scoreTextInGame;
        this.backgroundImage = backgroundImage;
        this.backgroundImage2 = backgroundImage2;
        this.backgroundImage3 = backgroundImage3;
        this.backgroundImage4 = backgroundImage4;
    }

    protected override void BackGroundImageManagement()
    {
        if (GameConfiguration.GetGameConfiguration().currentLevel == 1) scoreScript.resetScore();
        scoreTextInGame.text = PersistenceManager.currentChallengeScore.ToString();
        backgroundImage.SetActive(false);
        switch (GameConfiguration.currentChallenge)
        {
            case 1: backgroundImage2.SetActive(true); break;
            case 2: backgroundImage3.SetActive(true); break;
            case 3: backgroundImage4.SetActive(true); break;
        }
    }

    protected override void FlippingControlManagement()
    {
        GameObject gameObject = FindObjectOfType<GameManager>().gameObject;
        flippingControl = gameObject.AddComponent<FlippingControl>();
    }

    protected override void ScoreManagement()
    {
    }
}
