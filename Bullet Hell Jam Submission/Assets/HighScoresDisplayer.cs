using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static dreamloLeaderBoard;

public class HighScoresDisplayer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] highScoreTexts;

    [SerializeField] dreamloLeaderBoard dl;

    List<dreamloLeaderBoard.Score> HighScoresList;
    //List<dreamloLeaderBoard.Score> HighScoresList;
    // Start is called before the first frame update
    void OnEnable()
    {
        //dl.AddScore(PlayerPrefs.GetString("Name"), PlayerPrefs.GetInt("highscore"));

        dl = FindObjectOfType<dreamloLeaderBoard>();
        //dl.GetScores();
        //dl.GetScores();
        //HighScoresList = dl.ToListHighToLow();
        //Debug.Log("count " + HighScoresList.Count);
        //StartCoroutine(GetScoresOnline());
        //dreamloLeaderBoard.AddScore("me", 10);

        for (int i = 0; i < highScoreTexts.Length; i++)

        {
            highScoreTexts[i].text = i + 1 + ". Fetching...";
            
        }
    }

    private void Update()
    {
        HighScoresList = dl.ToListHighToLow();
        for (int i = 0; i < highScoreTexts.Length; i++)

        {
            highScoreTexts[i].text = i + 1 + ". N/A...";
            if (HighScoresList.Count > i)
            {
                Debug.Log("yes");
                highScoreTexts[i].text = i + 1 + ". " + HighScoresList[i].playerName + " - " + HighScoresList[i].score;
            }
        }
    }

    //private IEnumerator GetScoresOnline()
    //{
    //    //while (true)
    //    //{
    //    //    //dl.GetScores();
    //    //    Debug.Log("Used");

    //    //    List<dreamloLeaderBoard.Score> HighScoresList = dl.ToListHighToLow();
    //    //    Debug.Log("count " + HighScoresList.Count);
    //    //    //dreamloLeaderBoard.AddScore("me", 10);

    //    //    for (int i = 0; i < highScoreTexts.Length; i++)

    //    //    {
    //    //        highScoreTexts[i].text = i + 1 + ". Fetching...";
    //    //        if (HighScoresList.Count > i)
    //    //        {
    //    //            Debug.Log("yes");
    //    //            highScoreTexts[i].text += HighScoresList[i].playerName;
    //    //        }
    //    //    }

    //    Debug.Log("running");
    //    while (true)
    //    {
    //        HighScoresList = dl.ToListHighToLow();
    //        for (int i = 0; i < highScoreTexts.Length; i++)

    //        {
    //            highScoreTexts[i].text = i + 1 + ". Fetching...";
    //            if (HighScoresList.Count > i)
    //            {
    //                Debug.Log("yes");
    //                highScoreTexts[i].text += HighScoresList[i].playerName;
    //            }
    //        }
    //        yield return new WaitForSeconds(1); 
    //    }




    //    //}
    //}

}
