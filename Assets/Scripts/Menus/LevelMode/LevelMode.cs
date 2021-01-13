using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Concurrent;

public class LevelMode : MonoBehaviour
{
    [SerializeField] Button lvl1Button;
    [SerializeField] Button lvl2Button;
    [SerializeField] Button lvl3Button;
    [SerializeField] Button lvl4Button;
    [SerializeField] Button lvl5Button;
    [SerializeField] Button lvl6Button;
    [SerializeField] Button lvl7Button;
    [SerializeField] Button lvl8Button;
    [SerializeField] Button lvl9Button;
    [SerializeField] Button lvl10Button;

    List<Button> buttonList = new List<Button>();
    Director director = new Director();

    string deck = "";

    void Start()
    {
        UnlockLevels(PersistenceManager.GetLastUnlockedLevel());
    }

    public void UnlockLevels(int level)
    {
        AddButtonsList();
        foreach (Button button in buttonList) {
            if (int.Parse(button.GetComponentInChildren<Text>().text) <= level)
            {
                button.interactable = true;
            }
        }
    }

    void AddButtonsList()
    {
        buttonList.Add(lvl1Button);
        buttonList.Add(lvl2Button);
        buttonList.Add(lvl3Button);
        buttonList.Add(lvl4Button);
        buttonList.Add(lvl5Button);
        buttonList.Add(lvl6Button);
        buttonList.Add(lvl7Button);
        buttonList.Add(lvl8Button);
        buttonList.Add(lvl9Button);
        buttonList.Add(lvl10Button);
    }

    public void Lvl1()
    {
        ConcretBuilderEasy lvl1 = new ConcretBuilderEasy("baraja numeros", 1);
        director.Create(lvl1);               
    }
     public void Lvl2()
    {
        ConcretBuilderEasy lvl2 = new ConcretBuilderEasy("baraja futbol", 2);
        director.Create(lvl2);
    }

     public void Lvl3()
     {
        ConcretBuilderEasy lvl3 = new ConcretBuilderEasy("baraja animales", 3);
        director.Create(lvl3);
     }

    public void Lvl4()
    {
        ConcretBuilderMedium lvl4 = new ConcretBuilderMedium("baraja alimentos", 4);
        director.Create(lvl4);
    }

    public void Lvl5()
    {
        ConcretBuilderMedium lvl5 = new ConcretBuilderMedium("baraja superheroes", 5);
        director.Create(lvl5);
    }

    public void Lvl6()
    {
        ConcretBuilderMedium lvl6 = new ConcretBuilderMedium("baraja padel", 6);
        director.Create(lvl6);
    }

    public void Lvl7()
    {
        ConcretBuilderMedium lvl7 = new ConcretBuilderMedium("baraja futbol", 7);
        director.Create(lvl7);
    }

    public void Lvl8()
    {
        ConcretBuilderHard lvl8 = new ConcretBuilderHard("baraja animales", 8);
        director.Create(lvl8);
    }

    public void Lvl9()
    {
        ConcretBuilderHard lvl9 = new ConcretBuilderHard("baraja superheroes", 9);
        director.Create(lvl9);
    }

    public void Lvl10()
    {  
        ConcretBuilderHard lvl10 = new ConcretBuilderHard("baraja padel", 10);
        director.Create(lvl10);
    }


    public void GoBack()
    {
     SceneManager.LoadScene("StartMenu");
    }

}


//Class DIRECTOR
 public class Director
{
 public Director() { }

 public void Create(BuilderNivel level) {
     level.CreateLevel();
     level.CreateRows();
     level.CreateColumns();
     level.CreateDeckName();
     level.CreateLastUnlockedLevel();

     GameConfiguration.SetGameConfiguration(level.GetLevel().GetRows(), level.GetLevel().GetColumns(), 0, 3, 0, 7, level.GetLevel().GetDeck(), GameMode.levelMode, level.GetLevel().GetLastLevel()) ;
     SceneManager.LoadScene("Game");
 }
}

//Class BuilderNivel
public abstract class BuilderNivel
{
    protected Level level;
    public Level GetLevel() { return level; }
    public void CreateLevel() { level = new Level(); }

    public abstract void CreateRows();
    public abstract void CreateColumns();
    public abstract void CreateDeckName();
    public abstract void CreateLastUnlockedLevel();
}


//Class LEVEL
public class Level {
    int rows;
    int columns;
    string deck;
    int lastLevel;

    public Level() { }
    public int GetRows() { return rows; }
    public int GetColumns() { return columns; }
    public string GetDeck() { return deck; }
    public int GetLastLevel() { return lastLevel; }


    public void SetRows(int rows) { this.rows = rows; }
    public void SetColumns (int columns) { this.columns = columns; }
    public void SetDeckName (string deck) { this.deck = deck; }
    public void SetLastLevel (int lastLevel) {this.lastLevel = lastLevel;}

}


//Todos los ConcretBuilderDificultad
public class ConcretBuilderEasy : BuilderNivel
{
    string deck;
    int lastUnlockedLevel;
    public ConcretBuilderEasy(string deck, int lastUnlockedLevel)
    {
        this.deck = deck;
        this.lastUnlockedLevel = lastUnlockedLevel;
    }

    public override void CreateRows() { level.SetRows(4); }
    public override void CreateColumns() { level.SetColumns(3); }
    public override void CreateDeckName() { level.SetDeckName(deck); }
    public override void CreateLastUnlockedLevel() { level.SetLastLevel(lastUnlockedLevel); }
}

public class ConcretBuilderMedium : BuilderNivel
{
    string deck;
    int lastUnlockedLevel;
    public ConcretBuilderMedium(string deck, int lastUnlockedLevel)
    {
        this.deck = deck;
        this.lastUnlockedLevel = lastUnlockedLevel;
    }

    public override void CreateRows() { level.SetRows(4); }
    public override void CreateColumns() { level.SetColumns(5); }
    public override void CreateDeckName() { level.SetDeckName(deck); }
    public override void CreateLastUnlockedLevel() { level.SetLastLevel(lastUnlockedLevel); }
}

public class ConcretBuilderHard : BuilderNivel
{
    string deck;
    int lastUnlockedLevel;
    public ConcretBuilderHard(string deck, int lastUnlockedLevel) {
        this.deck = deck;
        this.lastUnlockedLevel = lastUnlockedLevel;
    }

    public override void CreateRows() { level.SetRows(4); }
    public override void CreateColumns() { level.SetColumns(6); }
    public override void CreateDeckName() { level.SetDeckName(deck); }
    public override void CreateLastUnlockedLevel() { level.SetLastLevel(lastUnlockedLevel); }
}