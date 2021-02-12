using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlPScript : MonoBehaviour
{
    public void EnablePausing() => GameObject.Find("Canvas").GetComponent<PauseMenuS>().enabled = true;
    public void LoadNext() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
