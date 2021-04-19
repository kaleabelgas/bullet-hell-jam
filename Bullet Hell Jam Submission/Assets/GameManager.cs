using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float chanceToSpawnEnemy = 1;
    [SerializeField] private float chanceToPickSpawnPoint = 1;

    [SerializeField] private float timePerLevel = 1;

    [SerializeField] private ControllerUI controllerUI;

    [SerializeField] private dreamloLeaderBoard dreamloLeaderBoard;
    private float timeRemaining;

    public event Action NextLevel;
    private int highScore = 1;

    public int Level { get; private set; } = 1;

    
    void Start()
    {
        timeRemaining = timePerLevel;
        NextLevel += controllerUI.UpdateCurrentLevel;
        highScore = PlayerPrefs.GetInt("highscore");
        SpawnEnemies();
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
            timeRemaining = timePerLevel;
            Level++;
            if(Level > highScore)
            {
                highScore = Level;
                PlayerPrefs.SetInt("highscore", highScore);
            }

            if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                GameOver();

            Debug.Log("Current Level: " + Level);
            NextLevel.Invoke();
            ClearEnemies();
            ClearBullets();
            SpawnEnemies();
        }
    }

    GameObject EnemyChosen(List<GameObject> enemiesToChooseFrom)
    {
        GameObject chosenEnemy = enemiesToChooseFrom[0];

        for(int i = 0; i < enemiesToChooseFrom.Count; i++)
        {
            // RNG
            float randomNumber = UnityEngine.Random.Range(1f, 100f);
            if (randomNumber < chanceToSpawnEnemy)
            {
                chosenEnemy = enemiesToChooseFrom[i];
                Debug.Log(chosenEnemy);
                return chosenEnemy;
            }
        }
        return chosenEnemy;
    }

    List<Transform> SpawnPointsChosen(List<Transform> spawnPointsToChooseFrom)
    {
        List<Transform> chosenSpawnPoints = new List<Transform>();

        // spawn in at least 1
        chosenSpawnPoints.Add(spawnPointsToChooseFrom[UnityEngine.Random.Range(1, spawnPointsToChooseFrom.Count)]);

        for(int i = 0; i < spawnPointsToChooseFrom.Count; i++)
        {
            float randomNumber = UnityEngine.Random.Range(1f, 100f);
            if (randomNumber < chanceToPickSpawnPoint)
            {
                chosenSpawnPoints.Add(spawnPointsToChooseFrom[i]);
            }
            Debug.Log(chosenSpawnPoints.Count);
        }
        return chosenSpawnPoints;
    }

    private void SpawnEnemies()
    {
        List<Transform> spawnPointsLocal = SpawnPointsChosen(spawnPoints);

        GameObject enemyLocal = EnemyChosen(enemies);

        for(int i = 0; i < spawnPointsLocal.Count; i++)
        {
            Instantiate(enemyLocal, spawnPointsLocal[i].position, Quaternion.identity);

        }
    }

    private void ClearEnemies()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < enemyObjects.Length; i++)
        {
            Destroy(enemyObjects[i]);
        }
    }

    private void ClearBullets()
    {
        GameObject[] bulletObjects = GameObject.FindGameObjectsWithTag("Bullet");

        for (int i = 0; i < bulletObjects.Length; i++)
        {
            bulletObjects[i].SetActive(false);
        }
    }

    public void GameOver()
    {
        dreamloLeaderBoard.AddScore(PlayerPrefs.GetString("Name"), PlayerPrefs.GetInt("highscore"));
        SceneManager.LoadScene(0);
    }
}
