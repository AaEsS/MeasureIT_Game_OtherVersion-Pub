using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    MusicScript audioMScript;
    public int HP;
    bool deathAfterRevived = false;

    public AudioClip powerupPickupSound, fireSound, shieldSound;
    public AudioSource fireSoundPlayer, shieldSoundPlayer;
    bool fireSoundOnce = true;
    bool shieldSoundOnce = true;

    public bool playerHit = false;
    
    public bool onFire = false;
    public bool shielded = false;

    public GameObject particleFireEffect;

    public float firePTime = 5f;

    public Sprite me;
    public float shieldPTime = 5f;

    public Powerups powerupsScript;
    public GameObject[] powerupsBs;
    public GameObject canvas;
    public bool powerupBpos1 = false;
    public bool powerupBpos2 = false;
    public bool powerupBpos3 = false;
    public Button pauseB;

    public Joystick joystick;

    public GameObject healthBar;
    public GameManager gameManager;
    public Animator gameplayeUIAnimator;
    public InstantiateBullet instantiateBullet;
    public FacePlayer facePlayer;

    public int powerupsPickupCount = 0;

    private void Awake()
    {
        audioMScript = GameObject.Find("AudioM").GetComponent<MusicScript>();
    }

    private void Start()
    {
        fireSoundPlayer = gameObject.AddComponent<AudioSource>();
        shieldSoundPlayer = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HP = (int)healthBar.GetComponent<Slider>().value;

        ShieldPowerupStopper();
        FirePowerupStopper();

        if (gameManager.revived)
        {
            Invoke("PlayerRevived", 1f);
            deathAfterRevived = true;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(joystick.Horizontal, joystick.Vertical) / (Time.timeScale == 0 ? 1 : Time.timeScale) * 2.5f;

        if (Input.touchCount > 0)
            if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) return;
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Bullet"))
        {
            if (onFire == false)
            {
                if (HP == 1)
                {
                    if (deathAfterRevived == false)
                    {
                        Time.timeScale = 1f;
                        GetComponent<SpriteRenderer>().enabled = false;
                        facePlayer.enabled = false;
                        instantiateBullet.enabled = false;
                        foreach (var bllet in GameObject.FindGameObjectsWithTag("Bullet")) Destroy(bllet);
                        gameplayeUIAnimator.SetTrigger("AfterDeathTrig");
                        healthBar.GetComponent<Shake>().ShakeIt(0.8f);
                        healthBar.GetComponent<HealthBarScript>().SetHealth(HP - 1);
                        audioMScript.hajjamiLoop.Pause();
                        powerupsScript.enabled = false;
                        pauseB.interactable = false;
                    }
                    else
                    {
                        Time.timeScale = 1f;
                        GetComponent<SpriteRenderer>().enabled = false;
                        facePlayer.enabled = false;
                        instantiateBullet.enabled = false;
                        foreach (var bllet in GameObject.FindGameObjectsWithTag("Bullet")) Destroy(bllet);
                        canvas.GetComponent<ButtonS>().ShowScore();
                        healthBar.GetComponent<Shake>().ShakeIt(0.8f);
                        healthBar.GetComponent<HealthBarScript>().SetHealth(HP - 1);
                        audioMScript.hajjamiLoop.Pause();
                        powerupsScript.enabled = false;
                        pauseB.interactable = false;
                    }
                }
                else
                {
                    GetComponent<ShakePlayer>().ShakeIt(0.3f);
                    healthBar.GetComponent<Shake>().ShakeIt(0.5f);
                    healthBar.GetComponent<HealthBarScript>().SetHealth(HP - 1);
                    GetComponent<Animator>().SetBool("PlayerHit", true);
                    playerHit = true;
                }
            }
        }

        if (powerupsPickupCount < 3)
        {
            if (trig.gameObject.name == "heart(Clone)")
            {
                AudioSource.PlayClipAtPoint(powerupPickupSound, transform.position);
                powerupsPickupCount++;
                if (powerupBpos1 && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject heartB = Instantiate(powerupsBs[0]);
                    heartB.transform.SetParent(canvas.transform, false);
                    heartB.transform.position = powerupsBs[2].transform.position;
                    heartB.SetActive(true);
                    powerupBpos3 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject heartB = Instantiate(powerupsBs[0]);
                    heartB.transform.SetParent(canvas.transform, false);
                    heartB.transform.position = powerupsBs[1].transform.position;
                    heartB.SetActive(true);
                    powerupBpos2 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject heartB = Instantiate(powerupsBs[0]);
                    heartB.transform.SetParent(canvas.transform, false);
                    heartB.transform.position = powerupsBs[0].transform.position;
                    heartB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject heartB = Instantiate(powerupsBs[0]);
                    heartB.transform.SetParent(canvas.transform, false);
                    heartB.transform.position = powerupsBs[0].transform.position;
                    heartB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject heartB = Instantiate(powerupsBs[0]);
                    heartB.transform.SetParent(canvas.transform, false);
                    heartB.transform.position = powerupsBs[0].transform.position;
                    heartB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3)
                {
                    GameObject heartB = Instantiate(powerupsBs[0]);
                    heartB.transform.SetParent(canvas.transform, false);
                    heartB.transform.position = powerupsBs[0].transform.position;
                    heartB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject heartB = Instantiate(powerupsBs[0]);
                    heartB.transform.SetParent(canvas.transform, false);
                    heartB.transform.position = powerupsBs[1].transform.position;
                    heartB.SetActive(true);
                    powerupBpos2 = true;
                }

                Destroy(trig.gameObject);
            }

            if (trig.gameObject.name == "shield(Clone)")
            {
                AudioSource.PlayClipAtPoint(powerupPickupSound, transform.position);
                powerupsPickupCount++;
                if (powerupBpos1 && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1]);
                    shieldB.transform.SetParent(canvas.transform, false);
                    shieldB.transform.position = powerupsBs[2].transform.position;
                    shieldB.SetActive(true);
                    powerupBpos3 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1]);
                    shieldB.transform.SetParent(canvas.transform, false);
                    shieldB.transform.position = powerupsBs[1].transform.position;
                    shieldB.SetActive(true);
                    powerupBpos2 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1]);
                    shieldB.transform.SetParent(canvas.transform, false);
                    shieldB.transform.position = powerupsBs[0].transform.position;
                    shieldB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1]);
                    shieldB.transform.SetParent(canvas.transform, false);
                    shieldB.transform.position = powerupsBs[0].transform.position;
                    shieldB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1]);
                    shieldB.transform.SetParent(canvas.transform, false);
                    shieldB.transform.position = powerupsBs[0].transform.position;
                    shieldB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1]);
                    shieldB.transform.SetParent(canvas.transform, false);
                    shieldB.transform.position = powerupsBs[0].transform.position;
                    shieldB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1]);
                    shieldB.transform.SetParent(canvas.transform, false);
                    shieldB.transform.position = powerupsBs[1].transform.position;
                    shieldB.SetActive(true);
                    powerupBpos2 = true;
                }

                Destroy(trig.gameObject);
            }

            if (trig.gameObject.name == "fire(Clone)")
            {
                AudioSource.PlayClipAtPoint(powerupPickupSound, transform.position);
                powerupsPickupCount++;
                if (powerupBpos1 && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject fireB = Instantiate(powerupsBs[2]);
                    fireB.transform.SetParent(canvas.transform, false);
                    fireB.transform.position = powerupsBs[2].transform.position;
                    fireB.SetActive(true);
                    powerupBpos3 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject fireB = Instantiate(powerupsBs[2]);
                    fireB.transform.SetParent(canvas.transform, false);
                    fireB.transform.position = powerupsBs[1].transform.position;
                    fireB.SetActive(true);
                    powerupBpos2 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject fireB = Instantiate(powerupsBs[2]);
                    fireB.transform.SetParent(canvas.transform, false);
                    fireB.transform.position = powerupsBs[0].transform.position;
                    fireB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject fireB = Instantiate(powerupsBs[2]);
                    fireB.transform.SetParent(canvas.transform, false);
                    fireB.transform.position = powerupsBs[0].transform.position;
                    fireB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject fireB = Instantiate(powerupsBs[2]);
                    fireB.transform.SetParent(canvas.transform, false);
                    fireB.transform.position = powerupsBs[0].transform.position;
                    fireB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3)
                {
                    GameObject fireB = Instantiate(powerupsBs[2]);
                    fireB.transform.SetParent(canvas.transform, false);
                    fireB.transform.position = powerupsBs[0].transform.position;
                    fireB.SetActive(true);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject fireB = Instantiate(powerupsBs[2]);
                    fireB.transform.SetParent(canvas.transform, false);
                    fireB.transform.position = powerupsBs[1].transform.position;
                    fireB.SetActive(true);
                    powerupBpos2 = true;
                }

                Destroy(trig.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D trigOut)
    {
        if (trigOut.gameObject.CompareTag("Bullet"))
        {
            GetComponent<Animator>().SetBool("PlayerHit", false);
            playerHit = false;
        }
    }

    void PlayerRevived() => GetComponent<Animator>().SetBool("Reviving", false);

    void FirePowerupStopper()
    {
        if (onFire)
        {
            if (fireSoundOnce)
            {
                fireSoundPlayer.PlayOneShot(fireSound);
                fireSoundOnce = false;
            }
            firePTime -= Time.deltaTime;
            firePTime = Mathf.Clamp(firePTime, -1f, 6f);
        }
        if (firePTime <= 0f)
        {
            fireSoundPlayer.Stop();
            particleFireEffect.SetActive(false);
            fireSoundOnce = true;
            onFire = false;
        }
    }

    void ShieldPowerupStopper()
    {
        if (shielded)
        {
            if (shieldSoundOnce)
            {
                shieldSoundPlayer.PlayOneShot(shieldSound);
                shieldSoundOnce = false;
            }
            shieldPTime -= Time.deltaTime;
            shieldPTime = Mathf.Clamp(shieldPTime, -1f, 6f);
        }
        if (shieldPTime <= 0f)
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = me;
            shieldSoundPlayer.Stop();
            shieldSoundOnce = true;
            shielded = false;
            GetComponent<Animator>().enabled = true;
        }
    }
}
