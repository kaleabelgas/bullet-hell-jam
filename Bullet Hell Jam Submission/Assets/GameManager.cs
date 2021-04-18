using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float chanceToSpawnEnemy = 1;
    [SerializeField] private float chanceToPickSpawnPoint = 1;

    private float timePerLevel = 10;
    private float timeRemaining;

    public int Level { get; private set; } = 1;

    
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
            // TODO: Next Level Event Trigger
            // TODO: Subscribe IEnumerator SpawnEnemies
        }
    }

    List<GameObject> EnemiesChosen(List<GameObject> enemiesToChooseFrom)
    {
        List<GameObject> chosenEnemies = new List<GameObject>();
        
        chosenEnemies.Add(enemiesToChooseFrom[0]);

        for(int i = 0; i < enemiesToChooseFrom.Count; i++)
        {
            // RNG
            float randomNumber = Random.Range(1f, 100f);
            if (randomNumber < chanceToSpawnEnemy)
                chosenEnemies.Add(enemiesToChooseFrom[i]);
        }
        return chosenEnemies;
    }

    List<Transform> SpawnPointsChosen(List<Transform> spawnPointsToChooseFrom)
    {
        // spawn in at least 1
        List<Transform> chosenSpawnPoints = new List<Transform>();

        chosenSpawnPoints.Add(spawnPointsToChooseFrom[Random.Range(1, spawnPointsToChooseFrom.Count)]);

        for(int i = 0; i < spawnPointsToChooseFrom.Count; i++)
        {
            float randomNumber = Random.Range(1f, 100f);
            if (randomNumber < chanceToSpawnEnemy)
                chosenSpawnPoints.Add(spawnPointsToChooseFrom[i]);
        }
        return chosenSpawnPoints;
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < SpawnPointsChosen(spawnPoints).Count; i++)
        {
            GameObject enemyy = Instantiate(EnemiesChosen(enemies)[i], SpawnPointsChosen(spawnPoints)[i].position, Quaternion.identity);
        }
    }
}
