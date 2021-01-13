using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipping : MonoBehaviour
{
    Sprite backSide, symbol;

    FlippingControl flippingController;

    Coroutine waitingToCover;

    Boolean needAnimation;

    public void SetBackSideAndSymbol(Sprite backSide, Sprite symbol)
    {
        this.backSide = backSide;
        this.symbol = symbol;

        
        Cover();

        flippingController = FindObjectOfType<FlippingControl>();
    }

    void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>().sprite.Equals(backSide)) TryUncover();
            
    }

    public void Cover() {
        GetComponent<SpriteRenderer>().sprite = backSide;
        if (waitingToCover != null) {
            StopCoroutine(waitingToCover);
            waitingToCover = null;          
        }
    }

    public void CoverWithAnimation() {
        StartCoroutine(RotateUncoveredCard());
        if (waitingToCover != null)
        {
            StopCoroutine(waitingToCover);
            waitingToCover = null;
        }
    }

    public void CoverAfter(float waitingTime) {
        waitingToCover = StartCoroutine(DoCoverAfter(waitingTime));      
    }

    IEnumerator DoCoverAfter(float waitingTime) {
        yield return new WaitForSeconds(waitingTime);
        flippingController.setAfterExposingBool(true);
        CoverWithAnimation();
    }

    void TryUncover() {
        if (flippingController.TryFlipping(gameObject)) Uncover();
    }

    public void Uncover() {
        StartCoroutine(RotateCoveredCard());
    }

    public override bool Equals(object other)
    {
        return other is CardFlipping && this.symbol.Equals(((CardFlipping) other).symbol);
    }

    public Sprite GetSymbolSprite() { return symbol; }


    public IEnumerator RotateCoveredCard() {

        for (float i = 0f; i <= 90f; i += 10) 
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f) { GetComponent<SpriteRenderer>().sprite = symbol; }
            yield return new WaitForSeconds(0.01f);       
        }

        for (float i = 90f; i >= 0f; i -= 10)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator RotateUncoveredCard()
    {

        for (float i = 0f; i <= 90f; i += 10)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f) { GetComponent<SpriteRenderer>().sprite = backSide; }
            yield return new WaitForSeconds(0.01f);
        }

        for (float i = 90f; i >= 0f; i -= 10)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}


