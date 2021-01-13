using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] Animator scoreAnim;
    [SerializeField] Animator pointsAnim;
    [SerializeField] Text pointsTextAnim;

    [SerializeField] Text scoreTextInGame;
    [SerializeField] Text scoreLabel;
    [SerializeField] Image backgroundTOP;
    static int score = 0;

    Hashtable alreadyFlipUpgraded = new Hashtable();

    public void IncreaseScore() {
        
        score += 10;
        scoreTextInGame.text = score.ToString();
        PositivePoints();
    }

    public void TakeTurnPenalty()
    {
        if (score > -998) score -= 2;
        else minScoredAllowed();
        scoreTextInGame.text = score.ToString();
        pointsTextAnim.text = "-2";
        NegativePoints();
    }

    public void DecreaseScore(CardFlipping cardOne, CardFlipping cardTwo)
    {
        if (alreadyFlipUpgraded.ContainsKey(cardOne) || alreadyFlipUpgraded.ContainsKey(cardTwo))
        {

            if (!alreadyFlipUpgraded.ContainsKey(cardOne)) alreadyFlipUpgraded.Add(cardOne, 0);
            else alreadyFlipUpgraded[cardOne] = failedScore(cardOne) + 1;

            if (!alreadyFlipUpgraded.ContainsKey(cardTwo)) alreadyFlipUpgraded.Add(cardTwo, 0);
            else alreadyFlipUpgraded[cardTwo] = failedScore(cardOne) + 1;

            if (score - (failedScore(cardOne) + failedScore(cardTwo) + 1) >= -999) {

                
                score -= failedScore(cardOne) + failedScore(cardTwo) + 1;
                int acum_points = failedScore(cardOne) + failedScore(cardTwo) + 1;
                pointsTextAnim.text = "-" + acum_points;

            }
            else minScoredAllowed();
            scoreTextInGame.text = score.ToString();
            NegativePoints();
        }
        else {

            alreadyFlipUpgraded.Add(cardOne, 0);
            alreadyFlipUpgraded.Add(cardTwo, 0);

            if (score > -999) {

              pointsTextAnim.text = "-1";
               score -= 1;

            }
            else minScoredAllowed();
            scoreTextInGame.text = score.ToString();
            NegativePoints();
        }

    }

    public void resetScore()
    {
        score = 0;
    }

    int failedScore(CardFlipping card)
    {
        return (int) alreadyFlipUpgraded[card];
    }

    public String gameScore()
    {
        return scoreTextInGame.text;       
    }

    public void activeDesactive(Boolean s)
    {
        backgroundTOP.gameObject.SetActive(s);
        scoreTextInGame.gameObject.SetActive(s);
        scoreLabel.gameObject.SetActive(s);
    }

    public void minScoredAllowed()
    {
        score = -999;
    }
    public int getScore() {
        return score;    
    }


    public void NegativePoints()
    {
        StartCoroutine(IENegativePoints());
    }

    IEnumerator IENegativePoints()
    {
        pointsAnim.Play("SlideInNegative");
        scoreAnim.Play("ScoreNegative");

        yield return new WaitForSeconds(0f);
          
    }

    public void PositivePoints()
    {
        StartCoroutine(IEPositivePoints());
    }

    IEnumerator IEPositivePoints()
    {
        pointsTextAnim.text = "+10";
        pointsAnim.Play("SlideInPositive");
        scoreAnim.Play("ScorePositive");
        yield return new WaitForSeconds(0f);
    }
}
