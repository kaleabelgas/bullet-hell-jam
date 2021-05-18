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
    public void ExitGame()
    {
        Application.Quit();
    }
}
