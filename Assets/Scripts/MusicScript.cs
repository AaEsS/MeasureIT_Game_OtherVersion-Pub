using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance;
    public AudioSource hajjamiMain, hajjamiIntro, hajjamiLoop;
    public static bool mainPlayed = false;
    public static bool introPlayed = false;

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
        if (SceneManager.GetActiveScene().name == "Start" && mainPlayed == false)
        {
            hajjamiIntro.Stop();
            hajjamiLoop.Stop();
            hajjamiMain.Stop();

            hajjamiMain.Play();
            mainPlayed = true;
        }
        if (SceneManager.GetActiveScene().name == "1" && introPlayed == false)
        {
            hajjamiIntro.Stop();
            hajjamiLoop.Stop();
            hajjamiMain.Stop();

            hajjamiIntro.Play();
            hajjamiLoop.PlayDelayed(hajjamiIntro.clip.length);
            introPlayed = true;
        }
    }
}