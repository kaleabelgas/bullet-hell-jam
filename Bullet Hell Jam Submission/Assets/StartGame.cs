using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;
    private void Start()
    {
        highScore.text = "High Score: Level " + PlayerPrefs.GetInt("highscore").ToString();
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
