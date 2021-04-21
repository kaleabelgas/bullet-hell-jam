using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private GameObject playerInput;

    private string playerName = "";
    private void Start()
    {
        highScore.text = "High Score: <color=#FF0D0D>Level " + PlayerPrefs.GetInt("highscore").ToString() + "</color>";
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Name")))
        {
            playerInput.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name");
        }
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void SetPlayerName()
    {
        playerName = playerInput.GetComponent<TMP_InputField>().text;
        PlayerPrefs.SetString("Name", playerName);
        PlayerPrefs.SetInt("highscore", 1);
        Debug.Log(playerName);
    }

    public void EnterName()
    {
        playerInput.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "Enter Name";
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("highscore", 1);
        highScore.text = "High Score: <color=#FF0D0D>Level" + PlayerPrefs.GetInt("highscore").ToString() + "</color>";
        //Debug.Log("reset");
    }

    public void OpenDiscord()
    {
        Application.OpenURL("https://discord.gg/JkF6BJEAeC");
    }

    public void OpenBMAC()
    {
        Application.OpenURL("https://www.buymeacoffee.com/kaleandpearl");
    }

    public void ExitGame()
    {
        //Debug.Log("reset");
        Application.Quit();
    }
}
