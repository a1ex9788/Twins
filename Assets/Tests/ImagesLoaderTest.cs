using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ImagesLoaderTest
    {

        [Test]
        [TestCase("baraja numeros")]
        [TestCase("baraja padel")]
        [TestCase("baraja futbol")]
        public void DeckCreation(string deckName)
        {
            Sprite backSideSprite = Resources.Load<Sprite>("Images&Sprites/Decks/" + deckName + "/backSide");

            List<Sprite> symbols = new List<Sprite>();
            Sprite[] loadedSymbols = Resources.LoadAll<Sprite>("Images&Sprites/Decks/" + deckName + "/symbols");
            if (loadedSymbols.Length == 1) loadedSymbols = Resources.LoadAll<Sprite>("Images&Sprites/Decks/" + deckName + "/symbols/" + deckName);
            foreach (Sprite s in loadedSymbols)
            {
                symbols.Add(s);
            }
            Deck deck = new Deck(deckName, backSideSprite, symbols);

            Assert.AreEqual(deck, ImagesLoader.LoadDeck(deckName));
        }

        [Test]
        [TestCase("baraja numeros", 10)]
        [TestCase("baraja padel", 28)]
        [TestCase("baraja futbol", 20)]
        public void AllSymbolsLoaded(string deckName, int numberOfSymbols)
        {
            Deck d = ImagesLoader.LoadDeck(deckName);

            Assert.AreEqual(d.symbols.Count, numberOfSymbols);
        }

    }
}
