using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DBAccess
{
    string customConfigurationFilePath;
    string lastUnlockedLevelFilePath;
    string challengeConfigurationFilePath;
    string jsonString;

    static DBAccess miDBAccess;

    private DBAccess()
    {
        customConfigurationFilePath = Application.persistentDataPath + "/CustomConfiguration.json";
        lastUnlockedLevelFilePath = Application.persistentDataPath + "/LastUnlockedLevel.json";
        challengeConfigurationFilePath = Application.persistentDataPath + "/ChallengeConfiguration.json";

        if (!File.Exists(customConfigurationFilePath)
            || !File.Exists(lastUnlockedLevelFilePath)
            || !File.Exists(challengeConfigurationFilePath))
        {
            File.WriteAllText(customConfigurationFilePath, "{ \"rows\":4,\"columns\":6,\"limitTime\":0.0,\"exposingTime\"" +
                ":3.0,\"showTime\":0.0,\"turnTime\":7.0,\"deckName\":\"baraja animales\", \"gameMode\":0,\"level\":0}");
            File.WriteAllText(lastUnlockedLevelFilePath, "{ \"level\":1}");
            File.WriteAllText(challengeConfigurationFilePath, "{ \"maxScores\":[{\"maxScore\":0},{\"maxScore\":0},{\"maxScore\":0}]}");
        }
    }


    public static DBAccess GetDBAcces() {
        if (miDBAccess == null) miDBAccess = new DBAccess();
        return miDBAccess;
    }


    public GameConfiguration GetGameConfiguration()
    {
        jsonString = File.ReadAllText(customConfigurationFilePath);
        return JsonUtility.FromJson<GameConfiguration>(jsonString);
    }

    public void SaveConfiguration(GameConfiguration gameConfiguration)
    {
        jsonString = JsonUtility.ToJson(gameConfiguration);
        File.WriteAllText(customConfigurationFilePath, jsonString);
    }


    public int GetLastUnlockedLevel()
    {
        jsonString = File.ReadAllText(lastUnlockedLevelFilePath);
        return JsonUtility.FromJson<LevelDB>(jsonString).level;
    }

    public void SaveLastUnlockedLevel(int lastUnlockedLevel)
    {
        jsonString = JsonUtility.ToJson(new LevelDB(lastUnlockedLevel));
        File.WriteAllText(lastUnlockedLevelFilePath, jsonString);
    }    

    public MaxScoreList GetMaxScoreList() {
        jsonString = File.ReadAllText(challengeConfigurationFilePath);
        return JsonUtility.FromJson<MaxScoreList>(jsonString);
    }

    public void SaveMaxScoreList(MaxScoreList list) {
        jsonString = JsonUtility.ToJson(list);
        File.WriteAllText(challengeConfigurationFilePath, jsonString);
    }
}


public class LevelDB {
    public int level;

    public LevelDB(int l) { level = l; }
}


[System.Serializable]
public class MaxScore {
    public int maxScore;

    public MaxScore(int ms) { maxScore = ms; }
}

[System.Serializable]
public class MaxScoreList {
    public List<MaxScore> maxScores;

    public MaxScoreList(List<MaxScore> maxScores)
    {
        this.maxScores = maxScores;
    }
}
