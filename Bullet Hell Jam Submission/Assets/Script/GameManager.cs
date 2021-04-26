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

    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<EnemyType> enemies;
    [SerializeField] private BossEnemy boss;
    [SerializeField] private float chanceToPickSpawnPoint = 1;
    [SerializeField] private float amountToIncreaseChance = 1;

    [SerializeField] private float timePerLevel = 1;

    [SerializeField] private UIMainGame uIMainGame;
    PulseScript pulseScript;

    private float timeRemaining;

    private HealthSpawner healthSpawner;

    public event Action NextLevel;
    private int highScore = 1;
    private bool tutorialDone = false;
    public int Level { get; private set; } = 0;


    void Awake()
    {
        CameraShake.TargetPos = transform.position;

        healthSpawner = GetComponent<HealthSpawner>();

        Time.timeScale = 1;
        pulseScript = GetComponent<PulseScript>();
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Name")))
        {
            PlayerPrefs.SetString("Name", "AAA");
        }
        timeRemaining = timePerLevel;
        NextLevel += uIMainGame.UpdateCurrentLevel;
        highScore = PlayerPrefs.GetInt("highscore");
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
            //Debug.Log("Current Level: " + Level);
            if (!tutorialDone)
            {
                uIMainGame.DeleteTutorial();
                tutorialDone = true;
            }

            Level++;

            AudioManager.instance.Play("next level");
            Time.timeScale = 0.3f;

            NextLevel.Invoke();
            pulseScript.DoPulse();
            ClearBullets();
            timeRemaining = timePerLevel;
            healthSpawner.ClearHealth();
            StartCoroutine(SpawnEnemies());


            if (amountToIncreaseChance < 100)
            {
                chanceToPickSpawnPoint += amountToIncreaseChance;
            }


            if (Level > highScore)
            {
                highScore = Level;
            }

            if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                GameOver();
        }
    }

    List<string> ChooseEnemies(List<EnemyType> _enemiesToChooseFrom, BossEnemy _boss)
    {
        List<string> chosenEnemies = new List<string>();

        for (int i = 0; i < _enemiesToChooseFrom.Count; i++)
        {
            if (Level >= _enemiesToChooseFrom[i].levelToAppear)
            {
                float randomN = UnityEngine.Random.Range(1f, 100f);
                if (randomN < _enemiesToChooseFrom[i].enemyChance)
                {
                    chosenEnemies.Add(_enemiesToChooseFrom[i].enemyName);
                }
            }
        }

        if (chosenEnemies.Count < 1)
            chosenEnemies.Add(_enemiesToChooseFrom[0].enemyName);

        if(Level % _boss.frequency == 0)
        {
            chosenEnemies.Clear();
            chosenEnemies.Add(boss.bossName);
        }

        return chosenEnemies;
    }

    List<Transform> ChooseSpawnPoints(List<Transform> spawnPointsToChooseFrom)
    {
        List<Transform> chosenSpawnPoints = new List<Transform>();

        // spawn in at least 1

        for (int i = 0; i < spawnPointsToChooseFrom.Count; i++)
        {
            float randomNumber = UnityEngine.Random.Range(1f, 100f);
            if (randomNumber < chanceToPickSpawnPoint)
            {
                chosenSpawnPoints.Add(spawnPointsToChooseFrom[i]);
            }
            //Debug.Log(chosenSpawnPoints.Count);
        }
        
        if(chosenSpawnPoints.Count < 1)
            chosenSpawnPoints.Add(spawnPointsToChooseFrom[UnityEngine.Random.Range(1, spawnPointsToChooseFrom.Count)]);
        
        return chosenSpawnPoints;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 1;
        healthSpawner.SpawnHealth();

        List<Transform> _spawnPoints= ChooseSpawnPoints(spawnPoints);
        //string _enemy = ChooseEnemy(enemies, boss);


        //Debug.Log(enemyLocal.Count);
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            List<string> _enemies = ChooseEnemies(enemies, boss);
            for(int j = 0; j < _enemies.Count; j++)
            {
                ObjectPooler.Instance.SpawnFromPool(_enemies[j], _spawnPoints[i].position, Quaternion.identity);

            }
        }
    }

    private void ClearBullets()
    {
        GameObject[] bulletsInScene = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(GameObject bullet in bulletsInScene)
        {
            bullet.SetActive(false);
        }
    }
    public void GameOver()
    {
        PlayerPrefs.SetInt("highscore", highScore);
        Time.timeScale = 0;
        //EndScreen.SetActive(true);
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
    }
}
