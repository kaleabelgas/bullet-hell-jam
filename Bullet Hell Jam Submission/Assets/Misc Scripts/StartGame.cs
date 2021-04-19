using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private GameObject playerInput;

    private string playerName = "";
    private void Start()
    {
        highScore.text = "High Score: Level " + PlayerPrefs.GetInt("highscore").ToString();
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void SetPlayerName()
    {
        playerName = playerInput.GetComponent<TMP_InputField>().text;
        PlayerPrefs.SetString("Name", playerName);
        Debug.Log(playerName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
