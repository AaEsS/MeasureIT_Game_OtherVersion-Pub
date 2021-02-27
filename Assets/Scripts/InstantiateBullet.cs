using UnityEngine;

public class InstantiateBullet : MonoBehaviour
{
    AudioSource slowMoInSoundPlayer;
    public AudioClip slowMoInSound;
    
    [SerializeField]
    Rigidbody2D bullet;

    float fireRate;
    float nextFire;

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
        // if (SceneManager.GetActiveScene().name == "1" && PlayerPrefs.GetInt("lvlReached") < 2)
        CheckIfTimeToFire();
        // slowMoInSoundPlayer.PlayOneShot(slowMoInSound);
    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(bullet, transform.position, transform.rotation); //, ,Quaternion.identity
            nextFire = Time.time + fireRate;
        }
    }
}
