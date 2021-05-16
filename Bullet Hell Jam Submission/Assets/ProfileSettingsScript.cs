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
        highScore.text = $"{PlayerPrefs.GetInt(PlayerPrefs.GetString("Name", "AAA") + "score")}00";
        totalEnemiesKilled.text = $"{PlayerPrefs.GetInt(PlayerPrefs.GetString("Name", "AAA") + "enemieskilled")}";
        playerName.text = $"{PlayerPrefs.GetString("Name", "AAA")}";
    }

    public void SetUsername(string name)
    {
        PlayerPrefs.SetString("Name", name);
        highScore.text = $"{PlayerPrefs.GetInt(name + "score", 0)}00";
        totalEnemiesKilled.text = $"{PlayerPrefs.GetInt(name + "enemieskilled", 0)}";
        //ResetScore();
    }

    public void OpenLeaderboardScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ResetScore()
    {
        highScore.text = "0";
        totalEnemiesKilled.text = "0";
        PlayerPrefs.SetInt(PlayerPrefs.GetString("Name", "AAA") + "score", 0);
        PlayerPrefs.SetInt(PlayerPrefs.GetString("Name", "AAA") + "enemieskilled", 0);
    }


}
