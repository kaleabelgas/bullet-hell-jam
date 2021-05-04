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
    float _delay;

    private void OnEnable()
    {
        //waveReached.text = $"{gameManager.CurrentWave}";
        //enemiesKilled.text = $"{EnemyCounter.EnemiesKilled}";
        //currentScore.text = $"{StartCoroutine(CountUpScore(EnemyCounter.SessionScore))}00";
        //highScore.text = $"{PlayerPrefs.GetInt("highscore")}00";
        StartCoroutine(CountUpAllScore());
        
    }

    IEnumerator CountUpAllScore()
    {
        yield return CountUpScore(gameManager.CurrentWave, waveReached);
        yield return CountUpScore(EnemyCounter.EnemiesKilled, enemiesKilled);
        yield return CountUpScore(EnemyCounter.SessionScore, currentScore, true);
        yield return CountUpScore(PlayerPrefs.GetInt("highscore"), highScore, true);

    }

    IEnumerator CountUpScore(int _targetscore, TextMeshProUGUI _text, bool _isScore = false)
    {
        int _currentNumber = 0;
        Debug.Log(_text.text);

        if (_targetscore > 100)
            _delay = 0.0001f;
        else if (_targetscore > 50)
            _delay = 0.001f;
        else if (_targetscore > 20)
            _delay = 0.01f;
        else
            _delay = 0.1f;

        while (_currentNumber < _targetscore)
        {
            _currentNumber++;

            if (_targetscore - _currentNumber > 5)
                _currentNumber += 5;

            _text.text = $"{_currentNumber}";
            if (_isScore)
            {
                _text.text += "00";
            }


            

            yield return new WaitForSecondsRealtime(_delay);
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
