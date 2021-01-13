using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistenceManager
{
    static DBAccess miDBAccess;

    public static int currentChallengeScore = 0;
    public static float totalTimeChallenge = 0;
    public static int totalMistakesChallenge = 0;

    public static GameConfiguration GetGameConfiguration() {
        return GetDBAcces().GetGameConfiguration();
    }

    public static int GetLastUnlockedLevel() {
        return GetDBAcces().GetLastUnlockedLevel();
    }

    public static void SaveCustomGameConfiguration(GameConfiguration customGameConfiguration) {
        GetDBAcces().SaveConfiguration(customGameConfiguration);
    }

    public static void SaveNewUnlockedLevel(int lvl)
    {
        if (lvl > GetDBAcces().GetLastUnlockedLevel())
            GetDBAcces().SaveLastUnlockedLevel(lvl);
    }


    static DBAccess GetDBAcces() {
        if (miDBAccess == null) miDBAccess = DBAccess.GetDBAcces();
        return miDBAccess;
    }


    public static int GetChallengeMaxScore(int index)
    {
        return GetDBAcces().GetMaxScoreList().maxScores[index].maxScore;
    }

    public static void SaveChallengeMaxScore(int index)
    {
        MaxScoreList list = GetDBAcces().GetMaxScoreList();        
        list.maxScores[index].maxScore = currentChallengeScore;
        currentChallengeScore = 0;

        GetDBAcces().SaveMaxScoreList(list);
    }

    public static void RaiseChallengeCurrentScore(int score)
    {
        currentChallengeScore = score;
    }

    public static void RaiseTotalTimeChallenge(float time)
    {
        totalTimeChallenge += time;
    }

    public static void RaiseTotalMistakeChallenge(int mistakes)
    {
        totalMistakesChallenge += mistakes;
    }
}
