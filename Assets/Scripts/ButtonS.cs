using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonS : MonoBehaviour
{
    public Button restartB, lvlsB, exitB, refPrecisionL, refPrecisionR, pauseB;
    public GameObject musicOnB, musicOffB, reflecionsWarning, instructions;

    public bool stopTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        int refsWarnTracker = PlayerPrefs.GetInt("refsWarnTracker", 0);

        GameObject.Find("ShootB").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height / Screen.width + 130f, Screen.height / Screen.width + 130f);

        GameObject.Find("ShootB").transform.position = new Vector2(Screen.width / 2, Screen.height / 20);

        if (PlayerPrefs.GetInt("lvlReached") > SceneManager.GetActiveScene().buildIndex - 1)
        {
            foreach (GameObject canon in GameObject.FindGameObjectsWithTag("Canon"))
                canon.GetComponent<CanonMvt>().frequency = 0.1f;
            GameObject.Find("ReflectionsCnt").transform.position = new Vector2(Screen.width / 2, Screen.height / 8);
            GameObject.Find("ReflectionsCnt").GetComponent<Text>().fontSize += Screen.height / Screen.width;
                
            GameObject.Find("Reflections").GetComponent<Text>().fontSize += Screen.height / Screen.width;
                
            GameObject.Find("ReflectionsS").GetComponent<RectTransform>().sizeDelta += new Vector2(Screen.height / Screen.width, Screen.height / Screen.width);

            GameObject.Find("ShootB").transform.position = new Vector2(Screen.width / 8, Screen.height / 20);

            if (SceneManager.GetActiveScene().buildIndex > 11)
            {
                GameObject.Find("CanonMvtSpeed").transform.position = new Vector2(Screen.width / 1.15f, Screen.height / 2.2f);
                GameObject.Find("CanonMvtSpeed").GetComponent<Text>().fontSize += Screen.height / Screen.width;

                GameObject.Find("Speed").GetComponent<Text>().fontSize += Screen.height / Screen.width;

                GameObject.Find("CanonMvtSS").GetComponent<RectTransform>().sizeDelta += new Vector2(Screen.height / Screen.width, Screen.height / Screen.width);
            }

            PlayerPrefs.SetInt("refsWarnTracker", PlayerPrefs.GetInt("refsWarnTracker") + 1);
            if (PlayerPrefs.GetInt("refsWarnTracker") <= 3) reflecionsWarning.SetActive(true);
        }

        if (SceneManager.GetActiveScene().name == "1" && PlayerPrefs.GetInt("lvlReached") < 2) instructions.SetActive(true);

        Button restart = restartB.GetComponent<Button>();
        restart.onClick.AddListener(RestartGameByB);
        Button lvls = lvlsB.GetComponent<Button>();
        lvls.onClick.AddListener(LoadLevelsByB);
        Button exit = exitB.GetComponent<Button>();
        exit.onClick.AddListener(LoadStartByB);

        Button musicOn = musicOnB.GetComponent<Button>();
        musicOn.onClick.AddListener(ToggleMusicOff);
        Button musicOff = musicOffB.GetComponent<Button>();
        musicOff.onClick.AddListener(ToggleMusicOn);

        if (PlayerPrefs.GetInt("musicTracker") == 0) ToggleMusicOn();
        else ToggleMusicOff();

        refPrecisionL = GameObject.Find("RefPBL").GetComponent<Button>();
        refPrecisionL.onClick.AddListener(DecreaseBy1Ref);
        refPrecisionR = GameObject.Find("RefPBR").GetComponent<Button>();
        refPrecisionR.onClick.AddListener(IncreaseBy1Ref);

        pauseB = GameObject.Find("PauseB").GetComponent<Button>();
        pauseB.onClick.AddListener(gameObject.GetComponent<PauseMenuS>().PauseGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Time.timeScale = 1f;
            gameObject.AddComponent<GameManS>().RestartLvl();
        }

        if (stopTimer == true && GameObject.Find("Timer") != null) GameObject.Find("Timer").GetComponent<TimerWin>().enabled = false;
    }

    public void RestartGameByB()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void LoadLevelsByB()
    {
        SceneManager.LoadScene("Lvls");
        Time.timeScale = 1f;
    }

    public void LoadStartByB()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1f;
    }

    void ToggleMusicOff()
    {
        musicOffB.SetActive(true);
        musicOnB.SetActive(false);
        GameObject.Find("AudioM").GetComponent<AudioSource>().volume = 0f;
        GameObject.Find("Player").GetComponent<AudioSource>().volume = 0f;
        PlayerPrefs.SetInt("musicTracker", 1);
    }

    void ToggleMusicOn()
    {
        musicOnB.SetActive(true);
        musicOffB.SetActive(false);
        GameObject.Find("AudioM").GetComponent<AudioSource>().volume = 0.696f;
        GameObject.Find("Player").GetComponent<AudioSource>().volume = 1f;
        PlayerPrefs.SetInt("musicTracker", 0);
    }

    void IncreaseBy1Ref() => GameObject.Find("ReflectionsS").GetComponent<Slider>().value += 1f;
    void DecreaseBy1Ref() => GameObject.Find("ReflectionsS").GetComponent<Slider>().value -= 1f;

    //public void ToggleSoundsOff()
    //{
    //    soundsOffB.SetActive(true);
    //    soundsOnB.SetActive(false);
    //}

    //public void ToggleSoundsOn()
    //{
    //    soundsOnB.SetActive(true);
    //    soundsOffB.SetActive(false);
    //}
}
