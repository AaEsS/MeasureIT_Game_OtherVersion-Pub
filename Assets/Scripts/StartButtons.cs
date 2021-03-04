using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtons : MonoBehaviour
{
    public GameObject musicB;
    public Sprite musicOnSprite, musicOffSprite;

    public void StartPlaying() => SceneManager.LoadScene("1");
    public void LoadLeaderboard() => GooglePlay.ShowLeaderboard(GPGSIds.leaderboard_best_score);
    public void GTFO() => Application.Quit();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Start")
            SceneManager.LoadScene("Start");
    }

    void Start()
    {
        if (!GameObject.Find("AudioM").GetComponent<AudioSource>().isPlaying)
        {
            GameObject.Find("AudioM").GetComponent<AudioSource>().Stop();
            GameObject.Find("AudioM").GetComponent<AudioSource>().Play();
        }

        if (PlayerPrefs.GetInt("musicTracker", 1) == 0) MusicOff();
        else MusicOn();
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

    public void BackToStart() => SceneManager.LoadScene("Start");
}
