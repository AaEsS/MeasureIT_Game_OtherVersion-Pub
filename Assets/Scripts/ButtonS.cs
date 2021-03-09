using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class ButtonS : MonoBehaviour
{
    public bool gamePaused = false;

    public GameObject player;
    public GameObject pauseMenuUI, musicB, submitBestScoreB;
    public Sprite musicOnSprite, musicOffSprite;
    public TextMeshProUGUI bestScoreText;
    public Animator gameplayeUIAnimator;

    public InstantiateBullet instantiateBullet;

    float sizeScaler = 0;
    bool scaleSize = false;

    bool scoreSubmitted = false;
    bool scoreRetrieved = false;

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

        if (gamePaused)
        {
            foreach (GameObject powerupB in GameObject.FindGameObjectsWithTag("PowerupButton"))
                powerupB.GetComponent<Button>().interactable = false;
            if (player.GetComponent<Controls>().shieldSoundPlayer.isPlaying) player.GetComponent<Controls>().shieldSoundPlayer.Pause();
            if (player.GetComponent<Controls>().fireSoundPlayer.isPlaying) player.GetComponent<Controls>().fireSoundPlayer.Pause();
        }
        else
        {
            foreach (GameObject powerupB in GameObject.FindGameObjectsWithTag("PowerupButton"))
                powerupB.GetComponent<Button>().interactable = true;
            if (!player.GetComponent<Controls>().shieldSoundPlayer.isPlaying) player.GetComponent<Controls>().shieldSoundPlayer.UnPause();
            if (!player.GetComponent<Controls>().fireSoundPlayer.isPlaying) player.GetComponent<Controls>().fireSoundPlayer.UnPause();
        }
            
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused) ResumeGame();
            else PauseGame();
        }

        if (player.GetComponent<SpriteRenderer>().enabled == false)
        {
            foreach (GameObject powerupB in GameObject.FindGameObjectsWithTag("PowerupButton"))
                powerupB.GetComponent<Button>().interactable = false;
        }

        if (scaleSize)
        {
            ShowScore();
            Invoke("ShowShotsSurvived", 1f);
        }
    }

    public void ShowScore()
    {
        scaleSize = true;
        gameplayeUIAnimator.SetTrigger("TimeUp");
        Invoke("ShowScoreAnimMethod", 1f);

        foreach (GameObject powerupB in GameObject.FindGameObjectsWithTag("PowerupButton"))
            Destroy(powerupB);

        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            if (scoreSubmitted == false)
            {
                Social.ReportScore(instantiateBullet.shots - 1, GPGSIds.leaderboard_best_score, success =>
                {
                    if (success) scoreSubmitted = true;
                    else Debug.Log("Failure");
                });
            }

            if (scoreSubmitted && scoreRetrieved == false)
            {
                PlayGamesPlatform.Instance.LoadScores(GPGSIds.leaderboard_best_score,
                LeaderboardStart.PlayerCentered, 1,
                LeaderboardCollection.Public,
                LeaderboardTimeSpan.AllTime,
                (LeaderboardScoreData data) =>
                {
                    bestScoreText.SetText($"best score\n{data.PlayerScore.formattedValue}");
                    scoreRetrieved = true;
                });
            }
            if (submitBestScoreB.activeSelf) submitBestScoreB.SetActive(false);
        }
        else
        {
            bestScoreText.SetText("");
            submitBestScoreB.SetActive(true);
        }
    }

    void ShowScoreAnimMethod()
    {
        gameplayeUIAnimator.SetTrigger("ShowScore");
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
        instantiateBullet.score.SetText($"{instantiateBullet.shots - 1}\n<size=%{sizeScaler}>shots\nsurvived</size>");
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
        GameObject.Find("AudioM").GetComponent<MusicScript>().hajjamiPlay.volume = 0.696f;
        PlayerPrefs.SetInt("musicTracker", 1);
    }
    void MusicOff()
    {
        musicB.GetComponent<Image>().sprite = musicOffSprite;
        GameObject.Find("AudioM").GetComponent<MusicScript>().hajjamiPlay.volume = 0f;
        PlayerPrefs.SetInt("musicTracker", 0);
    }

    public void RestartAfterDeath() => Invoke("Restart", 1f);
    void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
