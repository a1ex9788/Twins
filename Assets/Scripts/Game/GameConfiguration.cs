using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameConfiguration
{
    public int rows; //Tienen que ser públicos por obligación, sino la persistencia no funciona
    public int columns;
    public float limitTime;
    public float exposingTime;
    public float showTime;
    public float turnTime;

    public string deckName;

    public GameMode gameMode;
    public int currentLevel;



    static GameConfiguration customGameConfiguration = PersistenceManager.GetGameConfiguration(); //Para la persistencia
    static GameConfiguration gameConfiguration; //Para pasar info entre escenas


    static List<GameConfiguration> challenge1 = new List<GameConfiguration> {
        new GameConfiguration(),
        new GameConfiguration(4, 4, 40, 3, 0, 7, "baraja futbol", GameMode.challengeMode, 1),
        new GameConfiguration(4, 5, 50, 3, 0, 7, "baraja futbol", GameMode.challengeMode, 2),
        new GameConfiguration(4, 6, 60, 3, 0, 7, "baraja futbol", GameMode.challengeMode, 3),
        new GameConfiguration(4, 5, 40, 3, 0, 7, "baraja futbol", GameMode.challengeMode, 4),
        new GameConfiguration(4, 4, 30, 3, 0, 7, "baraja futbol", GameMode.challengeMode, 5)

    };

    static List<GameConfiguration> challenge2 = new List<GameConfiguration> {
        new GameConfiguration(),
        new GameConfiguration(4, 4, 40, 3, 0, 7, "baraja animales", GameMode.challengeMode, 1),
        new GameConfiguration(4, 5, 50, 3, 0, 7, "baraja animales", GameMode.challengeMode, 2),
        new GameConfiguration(4, 6, 60, 3, 0, 7, "baraja animales", GameMode.challengeMode, 3),
        new GameConfiguration(4, 5, 40, 3, 0, 7, "baraja animales", GameMode.challengeMode, 4),
        new GameConfiguration(4, 4, 30, 3, 0, 7, "baraja animales", GameMode.challengeMode, 5)
    };

    static List<GameConfiguration> challenge3 = new List<GameConfiguration> {
        new GameConfiguration(),
        new GameConfiguration(4, 4, 40, 3, 0, 7, "baraja alimentos", GameMode.challengeMode, 1),
        new GameConfiguration(4, 5, 50, 3, 0, 7, "baraja alimentos", GameMode.challengeMode, 2),
        new GameConfiguration(4, 6, 60, 3, 0, 7, "baraja alimentos", GameMode.challengeMode, 3),
        new GameConfiguration(4, 5, 40, 3, 0, 7, "baraja alimentos", GameMode.challengeMode, 4),
        new GameConfiguration(4, 4, 30, 3, 0, 7, "baraja alimentos", GameMode.challengeMode, 5)
    };

    public static List<List<GameConfiguration>> challenges = new List<List<GameConfiguration>> {
        new List<GameConfiguration>(),
        challenge1,
        challenge2,
        challenge3
    };

    public static int currentChallenge = 0;

    public GameConfiguration() { }

    GameConfiguration(int rows, int columns, float limitTime, float exposingTime,
        float showTime, float turnTime, string deckName, GameMode gameMode, int currentLevel)
    {
        this.rows = rows;
        this.columns = columns;
        this.limitTime = limitTime;
        this.exposingTime = exposingTime;
        this.showTime = showTime;
        this.turnTime = turnTime;
        this.deckName = deckName;
        this.gameMode = gameMode;
        this.currentLevel = currentLevel;
    }

    public static void SetChallengeGameConfiguration(int challenge, int level) {
        gameConfiguration = GameConfiguration.challenges[challenge][level];
        currentChallenge = challenge;
    }

    public static void SetCustomGameConfiguration(int rows, int columns, float limitTime, float exposingTime,
        float showTime, float turnTime, string deckName, GameMode gameMode, int currentLevel)
    {
        customGameConfiguration.rows = rows;
        customGameConfiguration.columns = columns;
        customGameConfiguration.limitTime = limitTime;
        customGameConfiguration.exposingTime = exposingTime;
        customGameConfiguration.showTime = showTime;
        customGameConfiguration.turnTime = turnTime;
        customGameConfiguration.deckName = deckName;
        customGameConfiguration.gameMode = gameMode;
        customGameConfiguration.currentLevel = currentLevel;

        PersistenceManager.SaveCustomGameConfiguration(customGameConfiguration);
    }

    public static void SetGameConfiguration(int rows, int columns, float limitTime, float exposingTime,
        float showTime, float turnTime, string deckName, GameMode gameMode, int currentLevel)
    {
        gameConfiguration = new GameConfiguration();

        gameConfiguration.rows = rows;
        gameConfiguration.columns = columns;
        gameConfiguration.limitTime = limitTime;
        gameConfiguration.exposingTime = exposingTime;
        gameConfiguration.showTime = showTime;
        gameConfiguration.turnTime = turnTime;
        gameConfiguration.deckName = deckName;
        gameConfiguration.gameMode = gameMode;
        gameConfiguration.currentLevel = currentLevel;
    }

    public static void SetCustomGameConfiguration(GameMode gameMode) {
        gameConfiguration = customGameConfiguration;
        gameConfiguration.gameMode = gameMode;
    }

    public static GameConfiguration GetGameConfiguration()
    {
        if (gameConfiguration == null) throw new System.Exception("Game configuration has not been set.");
        return gameConfiguration;
    }

    public int GetRows() { return rows; }
    public int GetColumns() { return columns; }
    public int GetNumCouples() { return rows * columns / 2; }
    public float GetLimitTime() { return limitTime; }
    public bool HasLimitTime() { return limitTime != 0; }
    public float GetExposingTime() { return exposingTime; }
    public float GetShowTime() { return showTime; }
    public bool HasShowTime() { return showTime != 0; }
    public float GetTurnTime() { return turnTime; }
    public string GetDeckName() { return deckName; }
    public bool IsFreeMode() { return gameMode == GameMode.standardGame || gameMode == GameMode.byCardGame || gameMode == GameMode.byCategoryGame; }
    public bool IsByCardGame() { return gameMode == GameMode.byCardGame; }
    public bool IsByCategoryGame() { return gameMode == GameMode.byCategoryGame; }
    public bool IsLevelMode() { return gameMode == GameMode.levelMode; }
    public bool IsChallengeMode() { return gameMode == GameMode.challengeMode; }
    public bool IsTournamentMode() { return gameMode == GameMode.tournamentMode; }
    public int GetCurrentLevel() { return currentLevel; }
}


public enum GameMode : int {
    standardGame,
    byCardGame,
    byCategoryGame,
    levelMode,
    challengeMode,
    tournamentMode
}
