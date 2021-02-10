using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManS : MonoBehaviour
{
    void Start()
    {
        Invoke("RestartLvl", 1f);
    }
    
    public void RestartLvl() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
