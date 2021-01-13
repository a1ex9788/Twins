using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagesLoader
{

    static String deckName;
    
    public static Deck LoadDeck(String deckName)
    {
        try
        {
            ImagesLoader.deckName = deckName;

            return new Deck(deckName, LoadBackSide(), LoadSymbols());
        }
        catch (Exception)
        {
            return new Deck(deckName, ImagesPersistence.LoadBackSide(deckName), ImagesPersistence.LoadCardsImages(deckName));
        }
    }

    static Sprite LoadBackSide() {
        return LoadSprite("backSide");
    }

    static List<Sprite> LoadSymbols()
    {
        List<Sprite> symbols = new List<Sprite>();

        Sprite[] loadedSymbols = Resources.LoadAll<Sprite>("Images&Sprites/Decks/" + deckName + "/symbols");
        if (loadedSymbols.Length == 1) loadedSymbols = Resources.LoadAll<Sprite>("Images&Sprites/Decks/" + deckName + "/symbols/" + deckName);
        foreach (Sprite s in loadedSymbols)
        {
            symbols.Add(s);
        }

        if (symbols.Count == 0) throw new Exception("Symbols '" + deckName + "' not found");

        return symbols;
    }

    static Sprite LoadSprite(string name) {
        Sprite s = Resources.Load<Sprite>("Images&Sprites/Decks/" + deckName + "/" + name);
        if (s == null) throw new Exception("Image '" + deckName + "/" + name + "' not found."); 
        return s;
    }


    public static int HowManyCards(String deckName) {
        try
        {
            ImagesLoader.deckName = deckName;
            return LoadSymbols().Count;
        }
        catch (Exception)
        {
            return ImagesPersistence.LoadCardsImages(deckName).Count;
        }
    }

}
