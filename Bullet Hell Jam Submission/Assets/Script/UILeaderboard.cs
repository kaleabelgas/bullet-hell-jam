using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILeaderboard : MonoBehaviour
{
    public void LoadMainMenu()
    {
        //AudioManager.instance.Stop("level moosic");
        AudioManager.instance.StopMusic();
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene(0);
    }
}
