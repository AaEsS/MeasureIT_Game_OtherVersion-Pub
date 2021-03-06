using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class ButtonS : MonoBehaviour
{
    public static bool gamePaused = false;

    public SpriteRenderer playerSpriteRender;
    public GameObject pauseMenuUI, musicB, submitBestScoreB;
    public Sprite musicOnSprite, musicOffSprite;
    public TextMeshProUGUI continueByAdText, bestScoreText;
    public Animator gameplayeUIAnimator;

    public float timeForContinue = 7f;

    public InstantiateBullet instantiateBullet;

    float sizeScaler = 0;
    bool scaleSize = false;

    bool scoreSubmitted = false;

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

        if (playerSpriteRender.enabled == false)
        {
            if (timeForContinue >= 0f)
            {
                timeForContinue -= Time.deltaTime;
                continueByAdText.SetText($"<size=%150>{Mathf.Round(timeForContinue)}</size>\n\n<size=%70>Continue?</size>\n<size=%40>(Ad)</size>");
            }
            else
            {
                gameplayeUIAnimator.SetTrigger("TimeUp");
                Invoke("ShowScore", 1f);
            }
        }

        if (scaleSize) Invoke("ShowShotsSurvived", 0.3f);
    }

    public void ShowScore()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated() && scoreSubmitted == false)
        {
            Social.ReportScore(instantiateBullet.shots - 1, GPGSIds.leaderboard_best_score, success =>
            {
                if (success) scoreSubmitted = true;
                else Debug.Log("Failure");
            });
        }

        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            if (scoreSubmitted)
            {
                PlayGamesPlatform.Instance.LoadScores(GPGSIds.leaderboard_best_score,
                LeaderboardStart.PlayerCentered, 1,
                LeaderboardCollection.Public,
                LeaderboardTimeSpan.AllTime,
                (LeaderboardScoreData data) =>
                {
                    bestScoreText.SetText($"best score\n{data.PlayerScore.formattedValue}");
                });
            }
            if (submitBestScoreB.activeSelf) submitBestScoreB.SetActive(false);
        }
        else
        {
            bestScoreText.SetText("");
            submitBestScoreB.SetActive(true);
        }
    
        //if (PlayerPrefs.GetInt("BestScore", 0) < instantiateBullet.shots) PlayerPrefs.SetInt("BestScore", instantiateBullet.shots - 1);
        scaleSize = true;
        gameplayeUIAnimator.SetTrigger("ShowScore");
        instantiateBullet.score.SetText($"{instantiateBullet.shots-1}\n<size=%{sizeScaler}>shots\nsurvived</size>");
    }

    public void SubmitScoreWhenSignedOut()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated()) submitBestScoreB.SetActive(false);
        else if (scoreSubmitted == false)
        {
            Social.localUser.Authenticate(suc =>
            {
                if (suc)
                {
                    Social.ReportScore(instantiateBullet.shots - 1, GPGSIds.leaderboard_best_score, success =>
                    {
                        if (success)
                        {
                            scoreSubmitted = true;
                            submitBestScoreB.SetActive(false);
                        }
                        else Debug.Log("Failure");
                    });
                }
                else Debug.Log("error");
            });
        }
    }

    void ShowShotsSurvived()
    {
        sizeScaler += 180f * Time.deltaTime;
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
