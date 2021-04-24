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

    WaitForSeconds leaderboardUpdate = new WaitForSeconds(0.5f);

    void OnEnable()
    {
        dl = FindObjectOfType<dreamloLeaderBoard>();
        dl.AddScore(PlayerPrefs.GetString("Name"), PlayerPrefs.GetInt("highscore"));
        for (int i = 0; i < highScoreTexts.Length; i++)
        {
            highScoreTexts[i].text = $"{i}. Fetching...";
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
        StartCoroutine(UpdateLeaderboard());
    }

    private IEnumerator UpdateLeaderboard()
    {

        HighScoresList = dl.ToListHighToLow();
        for (int i = 0; i < highScoreTexts.Length; i++)
        {
            highScoreTexts[i].text = $"[Fetching... ]";
            if (HighScoresList.Count > i)
            {
                //Debug.Log("yes");
                string textToDisplay = $"{HighScoresList[i].score} - {HighScoresList[i].playerName}";
                if(textToDisplay.Length > 16) { textToDisplay = $"{textToDisplay.Substring(0, 14)}.."; }
                highScoreTexts[i].text = $"[{textToDisplay}]";
            }
        }
        yield return leaderboardUpdate;
        
    }
}
