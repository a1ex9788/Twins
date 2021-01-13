using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class TurnTimeController : TimeController
{
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioClip loseTurnSound;

    AudioSource audioSource;

    Coroutine restartTurnCorutine;

    private void Start()
    {
        timeRemaining = gameManager.GetTurnTime();
        DisplayTime("turno");
    }

    public void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                if (gameManager.IsLimitTime() && this.timeRemaining > gameManager.GetRemainingTime())
                {
                    this.timeRemaining = gameManager.GetRemainingTime();
                }
                timeRemaining -= Time.deltaTime;
                DisplayTimeAddingOneSecond();
            }
            else
            {
                if (!AudioManager.getIsMutedEffect()) audioSource.PlayOneShot(loseTurnSound);
                gameManager.TurnPenalty();
                ResetTurn();
                gameManager.CoverFirstCard();
            }
        }
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void ResetTurn()
    {
        timeRemaining = gameManager.GetTurnTime();
        timerIsRunning = true;
    }

    public void ResetTurnAfter(float waitingTime)
    {
        if (restartTurnCorutine != null) StopCoroutine(restartTurnCorutine);
        restartTurnCorutine = StartCoroutine(DoResetTurnAfter(waitingTime));
    }

    public void KillTurnTimeCorutine() {
        if (restartTurnCorutine != null) {
            ResetTurn();
            StopCoroutine(restartTurnCorutine);
            restartTurnCorutine = null;
        }
    }

    IEnumerator DoResetTurnAfter(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        ResetTurn();
    }

    public void Pause()
    {
        timerIsRunning = false;
    }

    public void Restart()
    {
        timerIsRunning = true;
    }

    public void DisplayTimeAddingOneSecond()
    {
        base.DisplayTimeAddingOneSecond(timeRemaining, "turno");
    }

}