using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SymbolDealerTest
    {
        SymbolDealer sd;

        public void SetUp(string deckName, int cardsNumber) {
            sd = new SymbolDealer(ImagesLoader.LoadDeck(deckName), cardsNumber);
        }


        [Test]
        [TestCase("baraja numeros", 4)]
        [TestCase("baraja padel", 8)]
        [TestCase("baraja futbol", 10)]
        public void ExactNumberOfSymbols(string deckName, int cardsNumber)
        {
            SetUp(deckName, cardsNumber);

            Assert.DoesNotThrow(new TestDelegate(ConsumeSymbols));
            Assert.Throws<Exception>(new TestDelegate(GetSymbol));

            void ConsumeSymbols() {
                for (int i = 0; i < cardsNumber; i++)
                    GetSymbol();
            }

            void GetSymbol() {
                sd.GetNextSymbol();
            }
        }

        [Test]
        [TestCase("baraja numeros", 4)]
        [TestCase("baraja padel", 8)]
        [TestCase("baraja futbol", 10)]
        public void DifferentSymbols_JustOneCoupleOfEach(string deckName, int cardsNumber)
        {
            SetUp(deckName, cardsNumber);

            List<Sprite> symbols = new List<Sprite>();

            for (int i = 0; i < cardsNumber; i++)
                symbols.Add(sd.GetNextSymbol());

            foreach (Sprite s in symbols) {
                int count = 0;
                foreach (Sprite s2 in symbols)
                    if (s.Equals(s2)) count++;

                Assert.AreEqual(2, count);
            }
        }

        [Test]
        [TestCase("baraja numeros", 4)]
        [TestCase("baraja padel", 8)]
        [TestCase("baraja futbol", 10)]
        public void Randomness(string deckName, int cardsNumber)
        {
            ExecuteRandomness();

            IEnumerator ExecuteRandomness() {
                SetUp(deckName, cardsNumber);

                List<Sprite> symbols1 = new List<Sprite>();

                for (int i = 0; i < cardsNumber; i++)
                    symbols1.Add(sd.GetNextSymbol());


                yield return new WaitForSeconds(0.01f);


                SetUp(deckName, cardsNumber);

                List<Sprite> symbols2 = new List<Sprite>();

                for (int i = 0; i < cardsNumber; i++)
                    symbols2.Add(sd.GetNextSymbol());


                Assert.AreNotEqual(symbols1, symbols2);
            }
        }
    }
}
