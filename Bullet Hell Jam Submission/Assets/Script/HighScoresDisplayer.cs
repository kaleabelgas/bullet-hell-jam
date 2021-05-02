using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static dreamloLeaderBoard;

public class HighScoresDisplayer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] top10Scores;

    [SerializeField] dreamloLeaderBoard dl;

    [SerializeField] TextMeshProUGUI highScoreText;

    List<dreamloLeaderBoard.Score> HighScoresList;

    [SerializeField] Color nameColor = new Color(255, 0, 0);

    [ColorUsageAttribute(true, true)]
    [SerializeField] Color highestColor = new Color(252, 186, 3);

    [ColorUsageAttribute(true, true)]
    [SerializeField] Color highColor = new Color();

    [ColorUsageAttribute(true, true)]
    [SerializeField] Color mediumColor = new Color();

    [ColorUsageAttribute(true, true)]
    [SerializeField] Color lowColor = new Color();



    [SerializeField] List<string> easterEggNames = new List<string>();

    WaitForSeconds leaderboardUpdate = new WaitForSeconds(0.5f);

    private int highScore;

    void OnEnable()
    {
        highScore = PlayerPrefs.GetInt("highscore");
        dl = FindObjectOfType<dreamloLeaderBoard>();
        dl.AddScore(PlayerPrefs.GetString("Name"), highScore);

        highScoreText.text = $"HIGH SCORE: {PlayerPrefs.GetInt("highscore")}";
        

        for (int i = 0; i < top10Scores.Length; i++)
        {
            top10Scores[i].text = $"{i}. Fetching...";
        }

        foreach (string name in easterEggNames)
        {

            if (PlayerPrefs.GetString("Name").Equals(name))
            {
                highScoreText.color = highestColor;
            }
        }

        if (highScore >= 200)
        {
            highScoreText.color = highestColor;
        }
        else if (highScore >= 100 && highScore < 150)
        {
            highScoreText.color = highColor;
        }
        else if (highScore >= 50 && highScore < 100)
        {
            highScoreText.color = mediumColor;
        }
        else if (highScore >= 20 && highScore < 50)
        {
            highScoreText.color = lowColor;
        }
    }

    private void Update()
    {
        StartCoroutine(UpdateLeaderboard());
    }

    private IEnumerator UpdateLeaderboard()
    {

        HighScoresList = dl.ToListHighToLow();
        for (int i = 0; i < top10Scores.Length; i++)
        {
            top10Scores[i].text = $"[Fetching... ]";
            if (HighScoresList.Count > i)
            {
                //Debug.Log("yes");
                string textToDisplay = $"{HighScoresList[i].score} - {HighScoresList[i].playerName}";
                if (textToDisplay.Length > 20) { textToDisplay = $"{textToDisplay.Substring(0, 18)}.."; }
                top10Scores[i].text = $"[{textToDisplay}]";

                string _name = PlayerPrefs.GetString("Name");
                if (_name.Equals(HighScoresList[i].playerName))
                {
                    top10Scores[i].color = nameColor;
                }
            }
        }
        yield return leaderboardUpdate;

    }
}
