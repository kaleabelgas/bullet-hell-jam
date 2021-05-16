using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class UIMainGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentScore;
    [SerializeField] TextMeshProUGUI currentLevel;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI highScoreLeft;
    [SerializeField] GameObject tutorial;
    [SerializeField] Slider slider;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameManager gameManager;

    ColorAdjustments colorAdjustments;
    Volume volume;

    private bool paused = false;

    private int score = 0;
    private int level = 0;
    private int highScoreAtStart;

    private void Start()
    {
        //AudioManager.instance.Play("level moosic");


        //StartCoroutine(AudioManager.instance.StartPlaylist());
        highScoreAtStart = PlayerPrefs.GetInt(PlayerPrefs.GetString("Name", "AAA") + "score");
        AudioManager.instance.PlayMusic();
        currentScore.text = $"SCORE: {score}00";
        currentLevel.text = $"WAVE {level}";
        
        volume = FindObjectOfType<Volume>();

        if(volume != null)
            volume.profile.TryGet(out colorAdjustments);

    }

    public void UpdateScore()
    {
        score = EnemyCounter.SessionScore;
        currentScore.text = $"SCORE: {score}00";
    }

    public void AddLevel()
    {
        level++;
        currentLevel.text = $"WAVE {level}";
    }

    public void DeleteTutorial()
    {
        tutorial.SetActive(false);
    }

    public void SetHealthBarMax(int healthmax)
    {
        slider.maxValue = healthmax;
        slider.value = healthmax;
    }

    public void SetHealthBar(int health)
    {
        slider.value = health;
    }

    public void Pause()
    {
        if(colorAdjustments != null)
            colorAdjustments.active = !colorAdjustments.active;
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            Debug.Log("Paused");
            pauseMenu.gameObject.SetActive(true);
            //AudioManager.instance.Play("level moosic");
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
            //AudioManager.instance.UnPause("level moosic");

        }

        highScoreLeft.text = $"{Mathf.Max(highScoreAtStart - score, 0)}00 points to go until HIGH SCORE";
    }

    public void GoToMainMenu()
    {
        //AudioManager.instance.Stop("level moosic");
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        timer.text = $"{Mathf.CeilToInt(gameManager.WaveTimer)}";
    }
}
