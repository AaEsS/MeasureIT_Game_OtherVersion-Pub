using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenuS : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    GameObject[] shooters;
    public Button resumeB;

    // Start is called before the first frame update
    void Start()
    {
        shooters = GameObject.FindGameObjectsWithTag("Shooter");
        Button resume = resumeB.GetComponent<Button>();
        resume.onClick.AddListener(ResumeGameByB);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused) ResumeGame();
            else PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    void ResumeGameByB()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
}
