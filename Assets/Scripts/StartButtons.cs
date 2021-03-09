﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;

public class StartButtons : MonoBehaviour
{
    public GameObject musicB;
    public Sprite musicOnSprite, musicOffSprite;
    public Text signText;

    public void StartPlaying() => SceneManager.LoadScene("1");
    public void SignInOutGooglePlay()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            PlayGamesPlatform.Instance.SignOut();
        }
        else
        {
            Social.localUser.Authenticate(suc =>
            {
                if (suc) Debug.Log("Sign in by button");
                else Debug.Log("error");
            });
        }
    }
    public void ShowLeaderboard()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Social.ShowLeaderboardUI();
        }
        else Social.localUser.Authenticate(successAuth =>
        {
            if (successAuth)
            {
                Social.ShowLeaderboardUI();
            }
            else Debug.Log("Failure to sign in");
        });
    }
    public void GTFO() => Application.Quit();

    void Update()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated()) signText.text = "Sign out of \nGoogle Play";
        else signText.text = "Sign in with \nGoogle Play";

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

    public void OpenYayaChannel() => Application.OpenURL("https://www.youtube.com/channel/UCmahZvO-m3b2Ib5318SmUHA");
    public void OpenYayaSoundcloud() => Application.OpenURL("https://soundcloud.com/yahya-hajjami");
}
