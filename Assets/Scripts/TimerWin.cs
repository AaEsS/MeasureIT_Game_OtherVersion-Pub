using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerWin : MonoBehaviour
{
    public Text timer;
    public float howMuchTime;
    AudioSource noiceSoundPlayer;
    public AudioClip noiceSound;

    bool noiceSoundOnce = true;

    public Animator nextLvlAnims;

    public GameObject pickupLoss;
    
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
        else
        {
            GameObject.Find("Canvas").GetComponent<PauseMenuS>().enabled = false;
            GameObject.Find("Player").GetComponent<ChooseSF>().enabled = false;

            if (noiceSoundOnce == true && GameObject.Find("Player").GetComponent<ChooseSF>().collectableCount >= 3)
            {
                nextLvlAnims.SetTrigger("GoToNext");
                LvlAfterUnlocked();
                noiceSoundPlayer.PlayOneShot(noiceSound);
                noiceSoundOnce = false;
            }
            if (GameObject.Find("Player").GetComponent<ChooseSF>().collectableCount < 3)
            {
                pickupLoss.SetActive(true);
                Invoke("RestartAfterPickupLoss", 2.2f);
            }
            if (PlayerPrefs.GetInt($"{SceneManager.GetActiveScene().name}StarsTracker", 0) < GameObject.Find("Player").GetComponent<ChooseSF>().collectableCount)
                PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().name}StarsTracker", GameObject.Find("Player").GetComponent<ChooseSF>().collectableCount);
        }
    }

    void RestartAfterPickupLoss() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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
