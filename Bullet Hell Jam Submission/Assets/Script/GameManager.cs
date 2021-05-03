using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public string enemyName;
        public int levelToAppear;
        public float enemyChance;
    }

    [System.Serializable]
    public class BossEnemy
    {
        public string bossName;
        public float frequency;
    }

    [System.Serializable]
    public class SpawnMode
    {
        public List<Transform> SpawnPoints;
        public int SpawnPointAmount;
        public int LevelToAppear;
    }

    [SerializeField] private List<SpawnMode> spawnPointClass;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private List<EnemyType> enemies;
    [SerializeField] private BossEnemy boss;

    [SerializeField] private float timePerLevel = 1;

    [SerializeField] private UIMainGame uIMainGame;
    [SerializeField] private GameObject gameOverScreen;
    PulseScript pulseScript;

    private float timeRemaining;

    private HealthSpawner healthSpawner;
    private bool tutorialDone = false;

    public int CurrentWave { get; private set; } = 0;
    void Awake()
    {
        CameraShake.TargetPos = transform.position;
        EnemyCounter.ClearEnemiesKilledCountCurrent();

        healthSpawner = GetComponent<HealthSpawner>();

        gameOverScreen.gameObject.SetActive(false);

        Time.timeScale = 1;
        pulseScript = GetComponent<PulseScript>();
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Name")))
        {
            PlayerPrefs.SetString("Name", "AAA");
        }
        timeRemaining = timePerLevel;
        //StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        // timer

        if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            DoNextLevel();
        }
    }

    private void DoNextLevel()
    {
        //Debug.Log("Current Level: " + Level);
        if (!tutorialDone)
        {
            uIMainGame.DeleteTutorial();
            tutorialDone = true;
        }
        EnemyCounter.SessionScore += 2; ; //yeah magic number, just add score on next wave lol
        uIMainGame.UpdateScore();
        AudioManager.instance.Play("next level");
        Time.timeScale = 0.3f;
        pulseScript.DoPulse();
        ClearBullets();

        CurrentWave++;

        timeRemaining = timePerLevel;
        healthSpawner.ClearHealth();
        StartCoroutine(SpawnEnemies());
    }

    List<string> ChooseEnemies(List<EnemyType> _enemiesToChooseFrom, BossEnemy _boss)
    {
        List<string> chosenEnemies = new List<string>();

        float randomN = UnityEngine.Random.Range(1f, 100f);
        for (int i = 0; i < _enemiesToChooseFrom.Count; i++)
        {
            if (CurrentWave >= _enemiesToChooseFrom[i].levelToAppear)
            {
                if (randomN < _enemiesToChooseFrom[i].enemyChance)
                {
                    chosenEnemies.Add(_enemiesToChooseFrom[i].enemyName);
                }
            }
        }

        if (chosenEnemies.Count < 1)
            chosenEnemies.Add(_enemiesToChooseFrom[0].enemyName);

        if (CurrentWave % _boss.frequency == 0)
        {
            chosenEnemies.Clear();
            chosenEnemies.Add(boss.bossName);
        }

        return chosenEnemies;
    }

    List<Transform> ChooseSpawnPoints(List<SpawnMode> _spawnMode)
    {
        // loop through the spawn modes
        List<Transform> chosenSpawnPoints = new List<Transform>();
        for(int i = 0; i < _spawnMode.Count; i++)
        {
            // check if the spawn mode will engage in that level
            if(CurrentWave >= _spawnMode[i].LevelToAppear)
            {
                chosenSpawnPoints.Clear();
                // choose spawnpoint depending on spawn mode's spawnpoint amount
                for(int j = 0; j < _spawnMode[i].SpawnPointAmount; j++)
                {
                    //randomize the location of spawnpoints
                    while (true)
                    {
                        int randomSpawn = UnityEngine.Random.Range(0, _spawnMode[i].SpawnPoints.Count);
                        if (chosenSpawnPoints.Contains(_spawnMode[i].SpawnPoints[randomSpawn]))
                            continue;
                        chosenSpawnPoints.Add(_spawnMode[i].SpawnPoints[randomSpawn]);
                        break;
                    }
                }
            }
        }
        return chosenSpawnPoints;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 1;
        healthSpawner.SpawnHealth();

        List<Transform> _spawnPoints = ChooseSpawnPoints(spawnPointClass);
        //string _enemy = ChooseEnemy(enemies, boss);


        //Debug.Log(enemyLocal.Count);
        for (int i = 0; i < _spawnPoints.Count;)
        {
            List<string> _enemies = ChooseEnemies(enemies, boss);
            for (int j = 0; j < Mathf.Min(_enemies.Count, _spawnPoints.Count); j++)
            {
                ObjectPooler.Instance.SpawnFromPool(_enemies[j], _spawnPoints[i].position, Quaternion.identity);
                i++;
            }
        }
    }

    private void ClearBullets()
    {
        GameObject[] bulletsInScene = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bulletsInScene)
        {
            bullet.SetActive(false);
        }
    }
    public void GameOver()
    {
        EnemyCounter.SavePlayerScores();
        Time.timeScale = 0;
        AudioManager.instance.StopMusic();
        PlayerPrefs.Save();
        gameOverScreen.gameObject.SetActive(true);
    }
}
