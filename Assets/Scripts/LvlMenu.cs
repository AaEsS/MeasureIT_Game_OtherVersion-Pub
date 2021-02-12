using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlMenu : MonoBehaviour
{
    public GameObject musicOnB, musicOffB, nextPage;

    public void LoadLvli(int Lvl) => SceneManager.LoadScene(Lvl+1);

    public Animator lvlPagesAnimator;

    bool wentOnce = false;

    void Start()
    {
        Button musicOn = musicOnB.GetComponent<Button>();
        musicOn.onClick.AddListener(ToggleMusicOff);
        Button musicOff = musicOffB.GetComponent<Button>();
        musicOff.onClick.AddListener(ToggleMusicOn);
        
        GameObject.Find("NextPageB").GetComponent<Button>().onClick.AddListener(GetNextLvlsPage);

        if (PlayerPrefs.GetInt("musicTracker") == 0) ToggleMusicOn();
        else ToggleMusicOff();

        if (!GameObject.Find("AudioM").GetComponent<AudioSource>().isPlaying)
            GameObject.Find("AudioM").GetComponent<AudioSource>().Play();

        if (PlayerPrefs.GetInt("lvlReached") > 20) GameObject.Find("NextPageB").GetComponent<Button>().interactable = true;
        else GameObject.Find("NextPageB").GetComponent<Button>().interactable = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Start");

        if (nextPage.activeSelf) GameObject.Find("PreviousLvlsPageB").GetComponent<Button>().onClick.AddListener(GetPreviousLvlsPage);
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

    void GetNextLvlsPage()
    {
        if (wentOnce == true) lvlPagesAnimator.SetTrigger("GoAgain");
        else
        {
            nextPage.SetActive(true);
            wentOnce = true;
        }
    }
    void GetPreviousLvlsPage() => lvlPagesAnimator.SetTrigger("BackPage");

    public void BackToStartB() => SceneManager.LoadScene("Start");
}
