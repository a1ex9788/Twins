using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinamicBoard : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    bool actived;
    int x, y, z;
    void Start()
    {
        z = 0;
        actived = true;
    }

    public void effect(int num)
    {
        if (num == 1) { VerticalPlaceCard(); }
        else if (num == 2) { HorizontalPlaceCard(); }
        else if (num == 3) { RotationPlaceCard(); }
    }

    public void VerticalPlaceCard()
    {
        x = 0;
        for (int i = 0; i < gameManager.GetRows(); i++)
        {
            if (!actived)
            {
                if (i == 0) { x = 18; }
                else if (i == 1) { x = 12; }
                else if (i == 2) { x = 6; }
                else if (i == 3) { x = 0; }
            }
            for (int j = 0; j < gameManager.GetColumns(); j++)
            {
                CardFlipping cardFlipping = gameManager.GetCardList()[x];
                PlaceCardFlippingCard(j * 1.5f, i * 1.2f, cardFlipping);
                x++;
            }
        }
        if (actived) { actived = false; }
        else { actived = true; }
    }
    public void HorizontalPlaceCard()
    {
        x = 0;
        for (int i = 0; i < gameManager.GetRows(); i++)
        {
            for (int j = 0; j < gameManager.GetColumns(); j++)
            {
                if (!actived)
                {
                    if (j == 0) { x = 5 + (i * 6); }
                    else if (j == 1) { x = 4 + (i * 6); }
                    else if (j == 2) { x = 3 + (i * 6); }
                    else if (j == 3) { x = 2 + (i * 6); }
                    else if (j == 4) { x = 1 + (i * 6); }
                    else if (j == 5) { x = 0 + (i * 6); }
                }
                CardFlipping cardFlipping = gameManager.GetCardList()[x];
                PlaceCardFlippingCard(j * 1.5f, i * 1.2f, cardFlipping);
                x++;
            }
        }
        if (actived) { actived = false; }
        else { actived = true; }
    }

    /* public void HorizontalPlaceCard2()
     {
         int x = 0;
         int y = 6;
         for (int i = 0; i < gameManager.GetColumns(); i++)
         {
             if (!actived)
             {
                 if (i == 0) { x = 5; }
                 else if (i == 1) { x = 4; }
                 else if (i == 2) { x = 3; }
                 else if (i == 3) { x = 2; }
                 else if (i == 4) { x = 1; }
                 else if (i == 5) { x = 0; }
             }
             for (int j = 0; j < gameManager.GetRows(); j++)
             {
                 Debug.Log("Valor de x: " + x + "  valor de J: "  + j + " valor de i:  " + i);
                 CardFlipping cardFlipping = gameManager.GetCardList()[x];
                 PlaceCardFlippingCard(j * 1.5f, i * 1.2f, cardFlipping);
                 if (!actived) x = x + y;
                 else x++;
             }
         }
         if (actived) { actived = false; }
         else { actived = true; }
     } */ //Optimitzar

    public void RotationPlaceCard() //+90º 4 quadrants (2x3)
    {
        x = 0;
        y = 0;
        for (int i = 0; i < gameManager.GetRows(); i++)
        {
            switch (z)
            {
                case 1: pas1i(i); break;
                case 2: pas2i(i); break;
                case 3: pas3i(i); break;
            }

            for (int j = 0; j < gameManager.GetColumns(); j++)
            {
                if (x - y == 3)
                {
                    switch (z)
                    {
                        case 1: pas1j(); break;
                        case 2: pas2j(); break;
                        case 3: pas3j(); break;
                        default:  break;    
                    }
                }
  
                CardFlipping cardFlipping = gameManager.GetCardList()[x];
                PlaceCardFlippingCard(j * 1.5f, i * 1.2f, cardFlipping);
                x++;
            }
        }
        z++;
        if (z == 4) { z = 0;}       
    }

    void pas1i(int i)
    {
        if (i == 0) { x = 3; y = 3; }
        else if (i == 1) { x = 9; y = 9; }
        else if (i == 2) { x = 0; y = 0; }
        else if (i == 3) { x = 6; y = 6; }
    }

    void pas1j()
    {
        if (y == 3) x = 15;
        else if (y == 9) x = 21;
        else if (y == 0) x = 12;
        else if (y == 6) x = 18;
    }

    void pas2i(int i)
    {
        if (i == 0) { x = 15; y = 15; }
        else if (i == 1) { x = 21; y = 21; }
        else if (i == 2) { x = 3; y = 3; }
        else if (i == 3) { x = 9; y = 9; }
    }

    void pas2j()
    {
        if (y == 15) x = 12;
        else if (y == 21) x = 18;
        else if (y == 3) x = 0;
        else if (y == 9) x = 6;
    }

    void pas3i(int i)
    {
        if (i == 0) { x = 12; y = 12; } //PAS 4
        else if (i == 1) { x = 18; y = 18; }
        else if (i == 2) { x = 15; y = 15; }
        else if (i == 3) { x = 21; y = 21; }
    }

    void pas3j()
    {
        if (y == 12) x = 0;
        else if (y == 18) x = 6;
        else if (y == 15) x = 3;
        else if (y == 21) x = 9;
    }

    void PlaceCardFlippingCard(float x, float y, CardFlipping newCard)
    {
        newCard.GetComponent<Transform>().localPosition = new Vector2(x, y);
    }


}
