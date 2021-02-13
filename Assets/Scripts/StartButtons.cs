using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtons : MonoBehaviour
{
    public GameObject musicOnB, musicOffB;

    public void StartPlaying() => SceneManager.LoadScene("1");
    public void LoadLevels() => SceneManager.LoadScene("Lvls");
    public void LoadHowToplay() => SceneManager.LoadScene("HowToPlay");
    public void GTFO() => Application.Quit();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Start")
            SceneManager.LoadScene("Start");
    }

    void Start()
    {
        Button musicOn = musicOnB.GetComponent<Button>();
        musicOn.onClick.AddListener(ToggleMusicOff);
        Button musicOff = musicOffB.GetComponent<Button>();
        musicOff.onClick.AddListener(ToggleMusicOn);

        if (PlayerPrefs.GetInt("musicTracker") == 0) ToggleMusicOn();
        else ToggleMusicOff();

        if (!GameObject.Find("AudioM").GetComponent<AudioSource>().isPlaying)
            GameObject.Find("AudioM").GetComponent<AudioSource>().Play();
    }

    void ToggleMusicOff()
    {
        musicOffB.SetActive(true);
        musicOnB.SetActive(false);
        GameObject.Find("AudioM").GetComponent<AudioSource>().volume = 0f;
        PlayerPrefs.SetInt("musicTracker", 1);
    }

    void ToggleMusicOn()
    {
        musicOnB.SetActive(true);
        musicOffB.SetActive(false);
        GameObject.Find("AudioM").GetComponent<AudioSource>().volume = 0.696f;
        PlayerPrefs.SetInt("musicTracker", 0);
    }

    public void BackToStart() => SceneManager.LoadScene("Start");
}
