using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<string> enemies;
    [SerializeField] private float chanceToSpawnEnemy = 1;
    [SerializeField] private float chanceToPickSpawnPoint = 1;

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

            pulseScript.DoPulse();
            StartCoroutine(SpawnEnemies());

            timeRemaining = timePerLevel;

            Level++;

            if (Level > highScore)
            {
                highScore = Level;
            }

            if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                GameOver();
        }
    }

    string EnemyChosen(List<string> enemiesToChooseFrom)
    {
        string chosenEnemy = enemiesToChooseFrom[0];

        for(int i = 0; i < enemiesToChooseFrom.Count; i++)
        {
            // RNG
            float randomNumber = UnityEngine.Random.Range(1f, 100f);
            if (randomNumber < chanceToSpawnEnemy)
            {
                chosenEnemy = enemiesToChooseFrom[i];
                //Debug.Log(chosenEnemy);
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
            //Debug.Log(chosenSpawnPoints.Count);
        }
        return chosenSpawnPoints;
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(.5f);

        List<Transform> spawnPointsLocal = SpawnPointsChosen(spawnPoints);
        Time.timeScale = 1;
        string enemyLocal = EnemyChosen(enemies);

        for(int i = 0; i < spawnPointsLocal.Count; i++)
        {
            ObjectPooler.Instance.SpawnFromPool(enemyLocal, spawnPointsLocal[i].position, Quaternion.identity);
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
