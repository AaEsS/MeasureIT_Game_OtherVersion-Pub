using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonS : MonoBehaviour
{
    public Button restartB, lvlsB, exitB, pauseB;
    public GameObject musicOnB, musicOffB;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("lvlReached") > SceneManager.GetActiveScene().buildIndex - 1)
        {
            foreach (GameObject canon in GameObject.FindGameObjectsWithTag("Canon"))
                canon.GetComponent<CanonMvt>().frequency = 0.1f;
        }

        if (PlayerPrefs.GetInt("musicTracker") == 0) ToggleMusicOn();
        else ToggleMusicOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            Time.timeScale = 1f;
            Restart();
        }
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

    public void ToggleMusicOff()
    {
        musicOffB.SetActive(true);
        musicOnB.SetActive(false);
        GameObject.Find("AudioM").GetComponent<AudioSource>().volume = 0f;
        GameObject.Find("Player").GetComponent<AudioSource>().volume = 0f;
        PlayerPrefs.SetInt("musicTracker", 1);
    }

    public void ToggleMusicOn()
    {
        musicOnB.SetActive(true);
        musicOffB.SetActive(false);
        GameObject.Find("AudioM").GetComponent<AudioSource>().volume = 0.696f;
        GameObject.Find("Player").GetComponent<AudioSource>().volume = 1f;
        PlayerPrefs.SetInt("musicTracker", 0);
    }

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

    public void RestartAfterDeath() => Invoke("Restart", 1f);
    void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
