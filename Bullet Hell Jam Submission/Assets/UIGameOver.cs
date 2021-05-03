using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI waveReached;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI enemiesKilled;

    private void OnEnable()
    {
        waveReached.text = $"{gameManager.CurrentWave}";
        highScore.text = $"{PlayerPrefs.GetInt("highscore")}00";
        enemiesKilled.text = $"{EnemyCounter.EnemiesKilled}";
        StartCoroutine(CountUpScore());
    }

    IEnumerator CountUpScore()
    {
        int targetScore = EnemyCounter.SessionScore;
        int _currentNumber = 0;

        while (_currentNumber < targetScore)
        {
            _currentNumber++;
            currentScore.text = $"{_currentNumber}00";
            yield return new WaitForSecondsRealtime(0.1f);

        }
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenLeaderBoard()
    {
        SceneManager.LoadScene(2);
    }
    public void RestartLevel()
    {
        EnemyCounter.ClearEnemiesKilledCountCurrent();
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);
    }
}
