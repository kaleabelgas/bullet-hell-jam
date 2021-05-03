using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ProfileSettingsScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI totalEnemiesKilled;
    [SerializeField] private TMP_InputField playerName;

    private void OnEnable()
    {
        highScore.text = $"{PlayerPrefs.GetInt("highscore")}00";
        totalEnemiesKilled.text = $"{PlayerPrefs.GetInt("enemieskilled")}";
        playerName.text = $"{PlayerPrefs.GetString("Name")}";
    }

    public void SetUsername(string name)
    {
        PlayerPrefs.SetString("Name", name);
        ResetScore();
    }

    public void OpenLeaderboardScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ResetScore()
    {
        highScore.text = "0";
        PlayerPrefs.SetInt("highscore", 0);
        totalEnemiesKilled.text = "0";
        PlayerPrefs.SetInt("enemieskilled", 0);
    }


}
