using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using MyUtils;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI waveReached;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    float _delay;

    int myHighScore;

    private void OnEnable()
    {

        StartCoroutine(CountUpAllScore());
        myHighScore = PlayerPrefs.GetInt(PlayerPrefs.GetString("Name", "AAA") + "score");


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopAllCoroutines();
            waveReached.text = FormatHelper.FormatNumber($"{gameManager.CurrentWave}", false, false);
            enemiesKilled.text = FormatHelper.FormatNumber($"{EnemyCounter.EnemiesKilled}", false, false);
            currentScore.text = FormatHelper.FormatNumber($"{EnemyCounter.SessionScore}00", false, false);
            highScore.text = FormatHelper.FormatNumber($"{myHighScore}00", false, false);
        }
    }

    IEnumerator CountUpAllScore()
    {
        yield return CountUpScore(gameManager.CurrentWave, waveReached);
        yield return CountUpScore(EnemyCounter.EnemiesKilled, enemiesKilled);
        yield return CountUpScore(EnemyCounter.SessionScore, currentScore, true);
        yield return CountUpScore(myHighScore, highScore, true);

    }

    IEnumerator CountUpScore(int _targetscore, TextMeshProUGUI _text, bool _isScore = false)
    {
        int _currentNumber = 0;
        //Debug.Log(_text.text);

        if (_targetscore > 100)
            _delay = 0.000001f;
        else if (_targetscore > 50)
            _delay = 0.001f;
        else if (_targetscore > 20)
            _delay = 0.01f;
        else
            _delay = 0.1f;

        while (_currentNumber < _targetscore)
        {
            _currentNumber++;

            if (_targetscore - _currentNumber > 50)
                _currentNumber += 50;

            _text.text = $"{_currentNumber}";
            if (_isScore)
            {
                _text.text += "00";
            }

            _text.text = FormatHelper.FormatNumber(_text.text, false, false);




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
