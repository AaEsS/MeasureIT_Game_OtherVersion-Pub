using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNext : MonoBehaviour
{
    public void LoadNext() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
