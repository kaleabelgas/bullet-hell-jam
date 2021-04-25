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
        for (int i = 0; i < highScoreTexts.Length; i++)
        {
            highScoreTexts[i].text = $"{i}. Fetching...";
        }

        foreach(string name in easterEggNames)
        {

            if(PlayerPrefs.GetString("Name").Equals(name))
            {
                gameOverText.color = highestColor;
            }
        }

        if(highScore >= 200)
        {
            gameOverText.color = highestColor;
        }
        else if (highScore >= 100 && highScore < 150)
        {
            gameOverText.color = highColor;
        }
        else if (highScore >= 50 && highScore < 100)
        {
            gameOverText.color = mediumColor;
        }
        else if (highScore >= 20 && highScore < 50)
        {
            gameOverText.color = lowColor;
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
