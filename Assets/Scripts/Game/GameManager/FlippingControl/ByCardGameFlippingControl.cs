using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ByCardGameFlippingControl : FlippingControl
{
    CardFlipping referenceCard;
    List<CardFlipping> cards;
    System.Random random = new System.Random();

    Image referenceCardImage;

    public override void Run()
    {
        base.Run();

        referenceCardImage = GameObject.Find("ReferenceCard").GetComponent<Image>();
        referenceCardImage.gameObject.SetActive(true);

        cards = gameManager.GetCardList();
        TakeNewReferenceCard();
    }

    protected override void DoCompareCards() {
        if (cardOne.Equals(cardTwo) && referenceCard.Equals(cardOne)) Hit();
        else Fail();
    }

    protected override void Hit() {
        base.Hit();
        TakeNewReferenceCard();
    }


    void TakeNewReferenceCard() {
        int randomIndex = random.Next(0, cards.Count);
        if (cards.Count > 0)
        {
            referenceCard = cards[randomIndex];
            RemoveCouple(randomIndex);
            ShowReferenceCard();
        }
    }

    void RemoveCouple(int index) {
        cards.RemoveAt(index);

        foreach (CardFlipping c in cards) {
            if (c.Equals(referenceCard)) { cards.Remove(c); break; }
        }
    }

    void ShowReferenceCard() {
        referenceCardImage.sprite = referenceCard.GetSymbolSprite();
    }
}
