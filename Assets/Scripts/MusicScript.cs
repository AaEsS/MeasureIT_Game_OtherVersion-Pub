using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    public AudioSource hajjamiMain, hajjamiPlay;
    static bool mainPlayed = false;
    static bool playPlayed = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "1" && playPlayed == false)
        {
            hajjamiMain.Stop();
            hajjamiPlay.Play();
            playPlayed = true;
            mainPlayed = false;
        }
        if (SceneManager.GetActiveScene().name == "Start" && mainPlayed == false)
        {
            hajjamiPlay.Stop();
            hajjamiMain.Play();
            mainPlayed = true;
            playPlayed = false;
        }
    }
}
