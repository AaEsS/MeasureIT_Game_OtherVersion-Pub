using UnityEngine;
using TMPro;

public class InstantiateBullet : MonoBehaviour
{
    AudioSource slowMoInSoundPlayer;
    public AudioClip slowMoInSound;
    
    [SerializeField]
    Rigidbody2D bullet;

    float fireRate;
    float nextFire;

    public TextMeshProUGUI score;
    int shots = 0;

    float timeToStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;

        slowMoInSoundPlayer = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToStart < 4.1f) timeToStart += Time.deltaTime;
        if (PlayerPrefs.GetInt("BestScore", 0) < shots) PlayerPrefs.SetInt("BestScore", shots - 1);
        // if (SceneManager.GetActiveScene().name == "1" && PlayerPrefs.GetInt("lvlReached") < 2)
        if (timeToStart > 4f) CheckIfTimeToFire();
        // slowMoInSoundPlayer.PlayOneShot(slowMoInSound);
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, transform.rotation); //, ,Quaternion.identity
            nextFire = Time.time + fireRate;
            score.SetText($"{shots}");
            shots++;
        }
    }
}
