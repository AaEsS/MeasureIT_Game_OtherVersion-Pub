using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenuS : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    GameObject[] shooters;
    public ChooseSF chooseSF;
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
            if (GamePaused)
            {
                ResumeGame();
                if (chooseSF.enabled == false)
                    foreach (GameObject shooter in shooters)
                        shooter.GetComponent<InstantiateBullet>().enabled = true;
                else
                {
                    foreach (GameObject shooter in shooters)
                        shooter.GetComponent<InstantiateBullet>().enabled = false;
                }
            }
            else
            {
                PauseGame();
                foreach (GameObject shooter in shooters)
                    shooter.GetComponent<InstantiateBullet>().enabled = false;
            }

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
        if (chooseSF.enabled == false)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GamePaused = false;
            foreach (GameObject shooter in shooters)
                shooter.GetComponent<InstantiateBullet>().enabled = true;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GamePaused = false;
        }
        
    }
}
