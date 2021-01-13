using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FlippingControl : MonoBehaviour
{
    protected GameManager gameManager;

    AudioClip foundCoupleSound;
    AudioClip flippingCardSound;
    AudioClip failedCoupleSound;

    AudioSource audioSource;
    protected CardFlipping cardOne, cardTwo;

    List<CardFlipping> waitingToBeCovered = new List<CardFlipping>();

    int numCouplesFound;
    int numFailures;

    Boolean afterExposing;

    public void Start()
    {
        numCouplesFound = 0;
        numFailures = 0;
        afterExposing = false;
    }

    public virtual void Run()
    {
        gameManager = FindObjectOfType<GameManager>();
        foundCoupleSound = Resources.Load<AudioClip>("Sounds/encontrarPareja");
        flippingCardSound = Resources.Load<AudioClip>("Sounds/voltearCartas");
        failedCoupleSound = Resources.Load<AudioClip>("Sounds/fallarPareja");

        audioSource = GetComponent<AudioSource>();
    }

    public bool TryFlipping(GameObject card)
    {
        bool canFlip = false;

        if (cardOne == null)
        {
            if (!AudioManager.getIsMutedEffect()) { audioSource.PlayOneShot(flippingCardSound); }
            CoverWaitingToBeCovered();
            gameManager.KillTurnTimeCorutine();

            cardOne = card.GetComponent<CardFlipping>();
            canFlip = true;
        }
        else if (cardTwo == null && !card.Equals(cardOne))
        {
            cardTwo = card.GetComponent<CardFlipping>();
            canFlip = true;
                        
            StartCoroutine(CompareCards());
        }

        return canFlip;
    }

    void CoverWaitingToBeCovered()
    {
        if (afterExposing)
        {
            afterExposing = false;
        }
        else
        {
            foreach (CardFlipping card in waitingToBeCovered)
            {
                card.CoverWithAnimation();
            }
        }
        waitingToBeCovered.Clear();
    }

    public void CoverFirstCard() {
        if (cardOne != null) {
            cardOne.CoverWithAnimation();
        }
        cardOne = null;
    }


    IEnumerator CompareCards() {
        yield return new WaitForSeconds(0);

        DoCompareCards();
        cardOne = null;
        cardTwo = null;
    }

    protected virtual void DoCompareCards() {
        if (cardOne.Equals(cardTwo)) Hit();
        else Fail();        
    }


    protected virtual void Hit() {
        gameManager.IncreaseScore();
        numCouplesFound++;
        if (numCouplesFound == gameManager.GetNumCouples())
        {
            if (gameManager.IsChallengeMode())
            {
                gameManager.ShowWinMenuChallenge();
            }
            else
            {
                gameManager.ShowWinMenu();
            }
        }
        else {

            if (!AudioManager.getIsMutedEffect()) { audioSource.PlayOneShot(foundCoupleSound); } 
        
        
        }

        gameManager.ResetTurn();
    }

    protected void Fail() {
        if (!AudioManager.getIsMutedEffect()) { audioSource.PlayOneShot(failedCoupleSound); }

        numFailures++;

        gameManager.DecreaseScore(cardOne, cardTwo);
        cardOne.CoverAfter(gameManager.GetExposingTime());
        cardTwo.CoverAfter(gameManager.GetExposingTime());

        waitingToBeCovered.Add(cardOne);
        waitingToBeCovered.Add(cardTwo);

        gameManager.PauseTurnTime();
        gameManager.ResetTurnAfter(gameManager.GetExposingTime());
    }

    public int GetNumFailures() { return numFailures; }

    public void setAfterExposingBool(bool afterExposing) { this.afterExposing = afterExposing; }
}
