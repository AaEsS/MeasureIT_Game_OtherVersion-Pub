using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LvlLock : MonoBehaviour
{
    public GameObject[] lvLBs;
    public Sprite rainbowButton;

    void Start()
    {
        int lvlReached = PlayerPrefs.GetInt("lvlReached", 1);
        for (int i = 0; i < lvLBs.Length; i++)
        {
            if (i + 1 > lvlReached) lvLBs[i].GetComponent<Button>().interactable = false;
        }

        for (int j = 0; j < PlayerPrefs.GetInt("lvlReached")-1; j++)
        {
            lvLBs[j].GetComponent<Image>().sprite = rainbowButton;
        }
    }
}
