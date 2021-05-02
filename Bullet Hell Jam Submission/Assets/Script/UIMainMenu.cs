using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    private void Start()
    {
        EnemyCounter.ClearEnemiesKilledCountCurrent();
    }
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
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
