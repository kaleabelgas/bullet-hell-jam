using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;

    [SerializeField] private TMP_InputField playerInput;

    private string playerName = "";
    private void Start()
    {
        AudioManager.instance.StopMusic();
        EnemyCounter.ClearEnemiesKilledCountCurrent();
        highScore.text = "High Score: <color=#FF0D0D>Level " + PlayerPrefs.GetInt("highscore").ToString() + "</color>";
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Name")))
        {
            playerInput.placeholder.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name");
        }
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void SetPlayerName(string nameInput)
    {
        playerName = playerInput.GetComponent<TMP_InputField>().text;
        Debug.Log(playerName);
        //playerName = nameInput;
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("Name", playerName);
            PlayerPrefs.SetInt("highscore", 1);
        }
        PlayerPrefs.Save();
    }

    public void EnterName()
    {
        playerInput.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "Enter Name";
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("highscore", 1);
        highScore.text = "High Score: <color=#FF0D0D>Level " + PlayerPrefs.GetInt("highscore").ToString() + "</color>";
        //Debug.Log("reset");
    }

    public void OpenLeaderboard()
    {
        SceneManager.LoadScene(2);
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
        Application.Quit();
    }
}
