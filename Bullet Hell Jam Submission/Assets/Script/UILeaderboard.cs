using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILeaderboard : MonoBehaviour
{
    public void LoadMainMenu()
    {
        //AudioManager.instance.Stop("level moosic");
        SceneManager.LoadScene(0);
    }
}
