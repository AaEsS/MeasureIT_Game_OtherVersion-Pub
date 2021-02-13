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
        for (int i = 0; i < lvLBs.Length; i++)
        {
            if (i + 1 > PlayerPrefs.GetInt("lvlReached", 1)) lvLBs[i].GetComponent<Button>().interactable = false;
        }

        for (int j = 0; j < PlayerPrefs.GetInt("lvlReached")-1; j++)
        {
            lvLBs[j].GetComponent<Image>().sprite = rainbowButton;
        }

        for (int k = 0; k < lvLBs.Length; k++)
        {
            for (int p = 1; p <= PlayerPrefs.GetInt($"{k + 1}StarsTracker"); p++)
            {
                Color clr = lvLBs[k].transform.GetChild(p).GetComponent<Image>().color;
                clr.a = 1f;
                lvLBs[k].transform.GetChild(p).GetComponent<Image>().color = clr;
            }
        }
    }
}
