using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerWin : MonoBehaviour
{
    public Text timer;
    public float howMuchTime;
    public GameObject nextLvl;
    AudioSource noiceSoundPlayer;
    public AudioClip noiceSound;

    bool noiceSoundOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = howMuchTime.ToString();
        noiceSoundPlayer = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (howMuchTime >= 0f)
        {
            howMuchTime -= Time.deltaTime;
            timer.text = Mathf.Round(howMuchTime).ToString();
        }
        else if (noiceSoundOnce == true)
        {
            nextLvl.SetActive(true);
            LvlAfterUnlocked();
            noiceSoundPlayer.PlayOneShot(noiceSound);
            noiceSoundOnce = false;
            GameObject.Find("Canvas").GetComponent<PauseMenuS>().enabled = false;
        }
    }

    public void StartTimer()
    {
        GameObject.Find("Timer").GetComponent<TimerWin>().enabled = true;
    }

    public void LvlAfterUnlocked()
    {
        if (PlayerPrefs.GetInt("lvlReached") < SceneManager.GetActiveScene().buildIndex)
            PlayerPrefs.SetInt("lvlReached", SceneManager.GetActiveScene().buildIndex);
    }
}
