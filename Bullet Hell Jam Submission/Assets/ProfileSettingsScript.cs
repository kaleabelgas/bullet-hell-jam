using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using MyUtils;

public class ProfileSettingsScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI totalEnemiesKilled;
    [SerializeField] private TMP_InputField playerName;

    private void OnEnable()
    {
        //highScore.text = $"{PlayerPrefs.GetInt(PlayerPrefs.GetString("Name", "AAA") + "score")}00";
        GetScores(PlayerPrefs.GetString("Name", "AAA"));

        //totalEnemiesKilled.text = $"{PlayerPrefs.GetInt(PlayerPrefs.GetString("Name", "AAA") + "enemieskilled")}";


        playerName.text = $"{PlayerPrefs.GetString("Name", "AAA")}";
    }

    public void SetUsername(string name)
    {
        PlayerPrefs.SetString("Name", name);
        GetScores(name);
        //ResetScore();
    }

    private void GetScores(string _name)
    {
        highScore.text = $"{PlayerPrefs.GetInt(_name + "score", 0)}00";

        highScore.text = FormatHelper.FormatNumber(highScore.text, false, false);

        totalEnemiesKilled.text = $"{PlayerPrefs.GetInt(_name + "enemieskilled", 0)}";

        totalEnemiesKilled.text = FormatHelper.FormatNumber(totalEnemiesKilled.text, false, false);
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

        Debug.Log(PlayerPrefs.GetInt(PlayerPrefs.GetString("Name", "AAA") + "enemieskilled"));
    }


}
