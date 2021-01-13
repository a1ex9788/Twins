using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreation : MonoBehaviour
{

    public GameObject cardPrefab;
    int rows, columns;
    Deck deck;

    SymbolDealer symbolDealer;

    List<CardFlipping> cardList = new List<CardFlipping>();    

    public void CreateCards(int rows, int columns, Deck deck)
    {
        this.rows = rows;
        this.columns = columns;
        this.deck = deck;
        symbolDealer = new SymbolDealer(deck, rows*columns);

        CorrectGridPosition();
        CreateCards();
    }


    void CreateCards() {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newCard = CreateCard();
                PlaceCard(j * 1.5f, i * 1.2f, newCard);
                GiveBackSideAndSymbol(newCard);

                cardList.Add(newCard.GetComponent<CardFlipping>());
            }
        }
    }

    void CorrectGridPosition() {
        float xOffset = -(columns-1)*0.75f;
        float yOffset = -(rows-1)*0.6f - 0.55f;
        GetComponent<Transform>().position = new Vector2(xOffset, yOffset);
    }

    GameObject CreateCard()
    {
        return Instantiate(cardPrefab, new Vector2(), new Quaternion(), GetComponent<Transform>());
    }

    void PlaceCard(float x, float y, GameObject newCard)
    {
        newCard.GetComponent<Transform>().localPosition = new Vector2(x, y);
    }

    void GiveBackSideAndSymbol(GameObject newCard)
    {
        Sprite symbol = symbolDealer.GetNextSymbol();
        newCard.GetComponent<CardFlipping>().SetBackSideAndSymbol(deck.backSide, symbol);
    }
  
    public List<CardFlipping> GetCardList() { return cardList; }

}
