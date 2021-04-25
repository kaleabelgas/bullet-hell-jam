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
        public float chanceToSpawn;
    }

    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<EnemyType> enemies;
    [SerializeField] private float chanceToPickSpawnPoint = 1;
    [SerializeField] private float amountToIncreaseChance = 1;

    [SerializeField] private float timePerLevel = 1;

    [SerializeField] private UIMainGame uIMainGame;
    PulseScript pulseScript;

    private float timeRemaining;

    public event Action NextLevel;
    private int highScore = 1;
    private bool tutorialDone = false;
    public int Level { get; private set; } = 0;

    
    void Awake()
    {
        CameraShake.TargetPos = transform.position;


        Time.timeScale = 1;
        pulseScript = GetComponent<PulseScript>();
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Name"))){
            PlayerPrefs.SetString("Name", "AAA");
        }
        timeRemaining = timePerLevel;
        NextLevel += uIMainGame.UpdateCurrentLevel;
        highScore = PlayerPrefs.GetInt("highscore");
        //SpawnEnemies();
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

            Time.timeScale = 0.3f;
            AudioManager.instance.Play("next level");

            NextLevel.Invoke();

            if (!tutorialDone)
            {
                uIMainGame.DeleteTutorial();
                tutorialDone = true;
            }

            if(amountToIncreaseChance < 100)
            {
                chanceToPickSpawnPoint += amountToIncreaseChance;
            }

            pulseScript.DoPulse();
            StartCoroutine(SpawnEnemies());

            timeRemaining = timePerLevel;

            Level++;

            if (Level > highScore)
            {
                highScore = Level;
            }

            //if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            //    GameOver();
        }
    }

    List<string> EnemyChosen(List<EnemyType> enemiesToChooseFrom)
    {
        List<string> chosenEnemy = new List<string>();

        for(int i = 0; i < enemiesToChooseFrom.Count; i++)
        {
            // RNG
            float randomNumber = UnityEngine.Random.Range(1f, 100f);
            if (randomNumber < enemiesToChooseFrom[i].chanceToSpawn)
            {
                chosenEnemy.Add(enemiesToChooseFrom[i].enemyName);
            }
        }
        if(chosenEnemy.Count <= 0) { chosenEnemy.Add(enemiesToChooseFrom[0].enemyName); }
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
            //Debug.Log(chosenSpawnPoints.Count);
        }
        return chosenSpawnPoints;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 1;

        List<Transform> spawnPointsLocal = SpawnPointsChosen(spawnPoints);
        List<string> enemyLocal = EnemyChosen(enemies);

        Debug.Log(enemyLocal.Count);
        for (int i = 0; i < spawnPointsLocal.Count; i++)
        {
            for (int j = 0; j < enemyLocal.Count; j++)
            {
                ObjectPooler.Instance.SpawnFromPool(enemyLocal[j], spawnPointsLocal[i].position, Quaternion.identity);
            }
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
