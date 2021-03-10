using UnityEngine;
using TMPro;

public class InstantiateBullet : MonoBehaviour
{
    public AudioClip slowMoInSound;
    
    [SerializeField]
    Rigidbody2D bullet;

    float fireRate;
    float nextFire;

    public TextMeshProUGUI score;
    public int shots = 0;

    float timeToStart = 0;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToStart < 7.7f) timeToStart += Time.deltaTime;
        if (timeToStart > 7.7f) CheckIfTimeToFire();
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
