using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck
{

    public string name;
    public Sprite backSide;
    public List<Sprite> symbols;

    public Deck(string name, Sprite backSide, List<Sprite> symbols)
    {
        this.name = name;
        this.backSide = backSide;
        this.symbols = symbols;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Deck)) return false;

        Deck other = ((Deck)obj);
        bool hasSameSprites = this.symbols.Count == other.symbols.Count;

        foreach (Sprite s in this.symbols) {
            int count = 0;
            foreach (Sprite s1 in other.symbols) {
                if (s != null && s.Equals(s1)) count++;
            }
            if (count != 1) { return false; }
        }

        return this.name.Equals(other.name)
            && this.backSide.Equals(other.backSide)
            && hasSameSprites;
    }

}