using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonS : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenuUI, musicB;
    public Sprite musicOnSprite, musicOffSprite;

    float timeForContinue = 7f;

    public InstantiateBullet shooter;

    float sizeScaler = 0;
    bool scaleSize = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("musicTracker") == 0) MusicOff();
        else MusicOn();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown("r"))
        {
            Time.timeScale = 1f;
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused) ResumeGame();
            else PauseGame();
        }

        if (GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled == false)
        {
            if (timeForContinue >= 0f)
            {
                timeForContinue -= Time.deltaTime;
                GameObject.Find("Continue?").GetComponent<TextMeshProUGUI>().SetText($"<size=%150>{Mathf.Round(timeForContinue)}</size>\n\n<size=%70>Continue?</size>\n<size=%40>(Ad)</size>");
            }
            else
            {
                GameObject.Find("GameplayUI").GetComponent<Animator>().SetTrigger("TimeUp");
                Invoke("ShowScore", 1f);
            }
        }

        if (scaleSize) Invoke("ShowShotsSurvived", 1f);
    }

    void ShowScore()
    {
        scaleSize = true;
        GameObject.Find("GameplayUI").GetComponent<Animator>().SetTrigger("ShowScore");
        GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>().SetText($"best score\n{PlayerPrefs.GetInt("BestScore")}");
        shooter.score.SetText($"{shooter.shots-1}\n<size=%{sizeScaler}>shots\nsurvived</size>");
    }

    void ShowShotsSurvived()
    {
        sizeScaler += 1f;
        sizeScaler = Mathf.Clamp(sizeScaler, 0, 60);
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void LoadLevels()
    {
        SceneManager.LoadScene("Lvls");
        Time.timeScale = 1f;
    }

    public void LoadStart()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
    }

    public void ToggleMusic()
    {
        if (PlayerPrefs.GetInt("musicTracker") == 0) MusicOn();
        else MusicOff();
    }

    void MusicOn()
    {
        musicB.GetComponent<Image>().sprite = musicOnSprite;
        GameObject.Find("AudioM").GetComponent<AudioSource>().volume = 0.696f;
        PlayerPrefs.SetInt("musicTracker", 1);
    }
    void MusicOff()
    {
        musicB.GetComponent<Image>().sprite = musicOffSprite;
        GameObject.Find("AudioM").GetComponent<AudioSource>().volume = 0f;
        PlayerPrefs.SetInt("musicTracker", 0);
    }

    public void RestartAfterDeath() => Invoke("Restart", 1f);
    void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
