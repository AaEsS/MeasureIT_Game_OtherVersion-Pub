using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonS : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenuUI, musicB;
    public Sprite musicOnSprite, musicOffSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("lvlReached") > SceneManager.GetActiveScene().buildIndex - 1)
        {
            foreach (GameObject canon in GameObject.FindGameObjectsWithTag("Canon"))
                canon.GetComponent<CanonMvt>().frequency = 0.1f;
        }
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
