using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyCounter
{
    public static int EnemiesKilled { get; private set; }
    public static int SessionScore { get; private set; }

    public static void ClearEnemiesKilledCountCurrent()
    {
        EnemiesKilled = 0;
    }

    public static void AddToScore(int amount)
    {
        SessionScore += amount;
        EnemiesKilled++;
    }

    public static void SavePlayerScores()
    {
        int enemiesKilledSaved = PlayerPrefs.GetInt("enemieskilled");
        PlayerPrefs.SetInt("enemieskilled", EnemiesKilled + enemiesKilledSaved);
        int _highscore = PlayerPrefs.GetInt("highscore");
        PlayerPrefs.SetInt("highscore", SessionScore > _highscore ? SessionScore : _highscore);
    }
}
