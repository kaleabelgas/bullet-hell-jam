using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using static dreamloLeaderBoard;

public class HighScoresDisplayer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI[] top10Names;
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
        highScore = PlayerPrefs.GetInt(PlayerPrefs.GetString("Name", "AAA") + "score");
        dl = FindObjectOfType<dreamloLeaderBoard>();

        dl.GetScores();

        highScoreText.text = $"LOCAL HIGH SCORE: {highScore}00";
        

        for (int i = 0; i < top10Names.Length; i++)
        {
            top10Names[i].text = $"Fetching...";
        }

        foreach (string name in easterEggNames)
        {

            if (PlayerPrefs.GetString("Name").Equals(name))
            {
                highScoreText.color = nameColor;
            }
        }
        ColorTheScore(highScoreText, highScore);
        
    }

    private void ColorTheScore(TextMeshProUGUI _text, int _score)
    {
        //Debug.Log(int.Parse(score.text));

        if (_score >= 5000)
        {
            _text.color = highestColor;
        }
        else if (_score >= 2500)
        {
            _text.color = highColor;
        }
        else if (_score >= 1000)
        {
            _text.color = mediumColor;
        }
        else
        {
            _text.color = lowColor;
        }
    }

    private void Update()
    {
        StartCoroutine(UpdateLeaderboard());
    }

    private IEnumerator UpdateLeaderboard()
    {

        HighScoresList = dl.ToListHighToLow();
        for (int i = 0; i < top10Names.Length; i++)
        {
            top10Names[i].text = $"Fetching...";
            if (HighScoresList.Count > i)
            {
                //Debug.Log("yes");
                string textToDisplay = $"{HighScoresList[i].playerName}";
                if (textToDisplay.Length > 40) { textToDisplay = $"{textToDisplay.Substring(0, 38)}.."; }
                top10Names[i].text = $"{textToDisplay}";

                string _name = PlayerPrefs.GetString("Name");
                if (_name.Equals(HighScoresList[i].playerName))
                {
                    top10Names[i].color = nameColor;
                }
            }
        }
        for (int i = 0; i < top10Scores.Length; i++)
        {
            top10Scores[i].text = $"0";
            if (HighScoresList.Count > i)
            {
                //Debug.Log("yes");
                string textToDisplay = $"{HighScoresList[i].score}00";
                if (textToDisplay.Length > 6) 
                { 
                    textToDisplay = $"{textToDisplay.Substring(0, 5)}M"; 
                }
                top10Scores[i].text = $"{textToDisplay}";

                ColorTheScore(top10Scores[i], HighScoresList[i].score);
            }
        }
        yield return leaderboardUpdate;

    }
}
