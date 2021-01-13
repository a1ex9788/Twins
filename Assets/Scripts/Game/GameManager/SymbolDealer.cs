using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SymbolDealer
{

    List<Sprite> symbols;
    int cardsNumber;

    System.Random random = new System.Random(System.DateTime.UtcNow.Millisecond);

    public SymbolDealer(Deck deck, int cardsNumber)
    {
        symbols = new List<Sprite>();
        this.cardsNumber = cardsNumber;

        FillList(deck.symbols);
    }


    void FillList(List<Sprite> deckSymbols)
    {
        int pairsNumber = cardsNumber / 2;
        if (cardsNumber % 2 != 0) pairsNumber++;
        if (pairsNumber > deckSymbols.Count) throw new Exception("Not enough symbols.");

        List<Sprite> availableSprites = deckSymbols.ToList();

        for (int i = 1; i <= pairsNumber; i++)
        {
            int index = GetRandomInt(0, availableSprites.Count);
            Sprite choosenSprite = availableSprites[index];
            availableSprites.RemoveAt(index);

            symbols.Add(choosenSprite);
            symbols.Add(choosenSprite);
        }
    }

    public Sprite GetNextSymbol()
    {
        if (symbols.Count == 0) throw new Exception("No more symbols available.");

        int index = GetRandomInt(0, symbols.Count);
        Sprite res = symbols[index];
        symbols.RemoveAt(index);
        return res;
    }

    int GetRandomInt(int lowerBound, int upperBound)
    {
        return random.Next(lowerBound, upperBound);
    }

}
