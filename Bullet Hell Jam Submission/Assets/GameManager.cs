using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float chanceToSpawnEnemy = 1;
    [SerializeField] private float chanceToPickSpawnPoint = 1;

    private float timePerLevel = 1;
    private float timeRemaining;

    public int Level { get; private set; } = 0;

    
    void Start()
    {
        timeRemaining = timePerLevel;
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
            Debug.Log("Current Level: " + Level);
            ClearEnemies();
            SpawnEnemies();
        }
    }

    GameObject EnemyChosen(List<GameObject> enemiesToChooseFrom)
    {
        GameObject chosenEnemy = enemiesToChooseFrom[0];

        for(int i = 0; i < enemiesToChooseFrom.Count; i++)
        {
            // RNG
            float randomNumber = Random.Range(1f, 100f);
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
        chosenSpawnPoints.Add(spawnPointsToChooseFrom[Random.Range(1, spawnPointsToChooseFrom.Count)]);

        for(int i = 0; i < spawnPointsToChooseFrom.Count; i++)
        {
            float randomNumber = Random.Range(1f, 100f);
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
}
