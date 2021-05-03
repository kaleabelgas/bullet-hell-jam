using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI enemiesKilled;

    private void OnEnable()
    {
        highScore.text = $"{PlayerPrefs.GetInt("highscore")}00";
        enemiesKilled.text = $"{EnemyCounter.EnemiesKilled}";
        currentScore.text = $"{EnemyCounter.SessionScore}";
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
