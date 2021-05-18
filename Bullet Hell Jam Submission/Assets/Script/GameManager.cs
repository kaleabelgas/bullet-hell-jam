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
    //[SerializeField] private List<SpawnMode> spawnPointClass;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private List<EnemyType> enemies;
    [SerializeField] private BossEnemy boss;

    [SerializeField] private float timePerLevel = 1;

    [SerializeField] private UIMainGame uIMainGame;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private dreamloLeaderBoard dl;
    PulseScript pulseScript;

    public float WaveTimer { get; private set; }
    private int levelScore = 0;

    private HealthSpawner healthSpawner;
    private bool tutorialDone = false;

    private bool isGameOver;


    private int enemyAmount = 1;

    public int CurrentWave { get; private set; } = 0;
    void Awake()
    {
        CameraShake.TargetPos = transform.position;
        EnemyCounter.ClearEnemiesKilledCountCurrent();
        dl = FindObjectOfType<dreamloLeaderBoard>();

        healthSpawner = GetComponent<HealthSpawner>();
        Time.timeScale = 1;
        pulseScript = GetComponent<PulseScript>();
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Name")))
        {
            PlayerPrefs.SetString("Name", "AAA");
        }
        WaveTimer = timePerLevel;
        //StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        // timer
        WaveTimer -= Time.deltaTime;

        if (WaveTimer <= 0)
        {
            DoNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver)
                uIMainGame.Pause();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //SkipWave();
            GameObject[] enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy");
            //Debug.Log(enemiesOnScreen.Length);

            if (enemiesOnScreen.Length.Equals(0)) { SkipWave(); }
        }
    }

    private void SkipWave()
    {
        if (isGameOver) { return; }
        EnemyCounter.SessionScore += (int)WaveTimer;
        DoNextLevel();
    }

    private void DoNextLevel()
    {
        //Debug.Log("Current Level: " + Level);

        levelScore = (int)(CurrentWave * .1f);


        if (!tutorialDone)
        {
            uIMainGame.DeleteTutorial();
            tutorialDone = true;
        }


        if (CurrentWave.Equals(0))
        {
            //Debug.Log("non");
        }
        else if((CurrentWave % boss.frequency).Equals(0))
        {
            EnemyCounter.SessionScore += 10 + levelScore;
        }
        else
        {
            EnemyCounter.SessionScore += levelScore;
        }

        CurrentWave++;

        enemyAmount = Mathf.CeilToInt(CurrentWave / 10f);

        uIMainGame.UpdateScore();
        uIMainGame.AddLevel();
        AudioManager.instance.Play("next level");
        Time.timeScale = 0.4f;
        pulseScript.DoPulse();
        ClearBullets();


        WaveTimer = timePerLevel;
        healthSpawner.ClearHealth();
        StartCoroutine(SpawnEnemies());
    }

    string ChooseEnemy(List<EnemyType> _enemiesToChooseFrom, BossEnemy _boss)
    {
        string _chosenEnemy = "";



        for (int i = 0; i < _enemiesToChooseFrom.Count; i++)
        {
            float randomN = UnityEngine.Random.Range(1f, 100f);


            if (CurrentWave >= _enemiesToChooseFrom[i].levelToAppear)
            {
                if (randomN < _enemiesToChooseFrom[i].enemyChance)
                {
                    _chosenEnemy = _enemiesToChooseFrom[i].enemyName;
                }
            }
        }

        if (string.IsNullOrEmpty(_chosenEnemy))
            _chosenEnemy = _enemiesToChooseFrom[0].enemyName;

        if (CurrentWave % _boss.frequency == 0)
        {
            _chosenEnemy = boss.bossName;
        }

        //Debug.Log("Chosen Enemies " + chosenEnemies.Count);
        return _chosenEnemy;
    }
    List<Transform> ChooseSpawnPointsNew(List<Transform> _transforms, int _amount)
    {

        //Debug.Log(_amount);

        List<Transform> _chosenSpawnPoints = new List<Transform>();

        for (int i = 0; i < _amount; i++)
        {
            int _randomSpawnPoint = UnityEngine.Random.Range(0, _transforms.Count - 1);

            _chosenSpawnPoints.Add(_transforms[_randomSpawnPoint]);
        }

        return _chosenSpawnPoints;
    }

    private IEnumerator SpawnEnemies()
    {
        healthSpawner.SpawnHealth();

        //List<Transform> _spawnPoints = ChooseSpawnPoints(spawnPointClass);
        ////string _enemy = ChooseEnemy(enemies, boss);


        ////Debug.Log(enemyLocal.Count);

        //for (int i = 0; i < _spawnPoints.Count;)
        //{
        //    List<string> _enemies = ChooseEnemies(enemies, boss);
        //    for (int j = 0; j < Mathf.Min(_enemies.Count, _spawnPoints.Count); j++)
        //    {
        //        ObjectPooler.Instance.SpawnFromPool(_enemies[j], _spawnPoints[i].position, Quaternion.identity);
        //        i++;
        //        yield return new WaitForSeconds(.3f);
        //        Time.timeScale = 1;
        //    }
        //}



        List<Transform> _spawnPoints = ChooseSpawnPointsNew(spawnPoints, enemyAmount);

        //List<string> _enemies = ChooseEnemy(enemies, boss, enemyAmount);

        //yield return new WaitForSeconds(.3f);

        WaitForSeconds waitForSecondsSmall = new WaitForSeconds(.001f);
        WaitForSeconds waitForSecondsBig = new WaitForSeconds(.3f);

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            //Debug.Log("Spawnpoints " + _spawnPoints.Count);

            string _enemy = ChooseEnemy(enemies, boss);

            ObjectPooler.Instance.SpawnFromPool(_enemy, _spawnPoints[i].position, Quaternion.identity);
            if (CurrentWave < 150) { yield return waitForSecondsBig; }
            else { yield return waitForSecondsSmall; }
            Time.timeScale = 1;
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
        PlayerPrefs.Save();
        AudioManager.instance.StopMusic();
        ClearBullets();
        isGameOver = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            ObjectPooler.Instance.SpawnFromPool("hit effect", enemy.transform.position, enemy.transform.rotation);
            enemy.gameObject.SetActive(false);
        }

        Time.timeScale = 0.2f;
        dl.AddScore(PlayerPrefs.GetString("Name"), EnemyCounter.SessionScore);
        //Debug.Log("game over");
        StartCoroutine(EndScreen());
    }

    IEnumerator EndScreen()
    {
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 0;
        gameOverScreen.gameObject.SetActive(true);
    }
}
