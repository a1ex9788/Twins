using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeController : TimeController
{
    [SerializeField] GameManager gameManager;
  
    private void Start()
    {
        timeRemaining = gameManager.GetLimitTime();
        DisplayTime("partida");
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTimeAddindOneSecond();
            }
            else
            {
                gameManager.ShowLoseMenu();
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }


    public void Pause()
    {
        timerIsRunning = false;
    }

    public void Restart()
    {
        timerIsRunning = true;
    }

    public float GetRemainingTime() { return timeRemaining; }

    public void DisplayTimeAddindOneSecond()
    {
        base.DisplayTimeAddingOneSecond(timeRemaining, "partida");
    }

    public void RestartGameAfter(float waitingTime)
    {
        StartCoroutine(DoRestartGameAfter(waitingTime));
    }

    IEnumerator DoRestartGameAfter(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        Restart();
    }
}