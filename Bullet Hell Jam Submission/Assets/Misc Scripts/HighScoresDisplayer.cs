using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static dreamloLeaderBoard;

public class HighScoresDisplayer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] highScoreTexts;

    [SerializeField] dreamloLeaderBoard dl;

    [SerializeField] TextMeshProUGUI gameOverText;

    List<dreamloLeaderBoard.Score> HighScoresList;

    [SerializeField] Color specialColor = new Color(252, 186, 3);

    [SerializeField] List<string> easterEggNames = new List<string>();

    void OnEnable()
    {
        dl = FindObjectOfType<dreamloLeaderBoard>();
        dl.AddScore(PlayerPrefs.GetString("Name"), PlayerPrefs.GetInt("highscore"));
        for (int i = 0; i < highScoreTexts.Length; i++)

        {
            highScoreTexts[i].text = i + 1 + ". Fetching...";
            
        }

        foreach(string name in easterEggNames)
        {

            if(PlayerPrefs.GetString("Name").Equals(name))
            {
                gameOverText.color = specialColor;
            }
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
                //Debug.Log("yes");
                highScoreTexts[i].text = i + 1 + ". " + HighScoresList[i].playerName + " - " + HighScoresList[i].score;
            }
        }
    }
}
