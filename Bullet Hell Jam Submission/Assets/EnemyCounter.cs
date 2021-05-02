using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyCounter
{
    public static int EnemiesKilled { get; private set; }

    public static void ClearEnemiesKilledCountCurrent()
    {
        EnemiesKilled = 0;
    }

    public static void AddEnemyToKillCount(int amount)
    {
        EnemiesKilled += amount;
    }
    public static void SaveEnemyKillCount()
    {
        int enemiesKilledSaved = PlayerPrefs.GetInt("enemieskilled");
        PlayerPrefs.SetInt("enemieskilled", EnemiesKilled + enemiesKilledSaved);
    }
}
