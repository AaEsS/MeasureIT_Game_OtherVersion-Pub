using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public int HP;
    bool deathAfterRevived = false;

    public GameObject[] powerupsBs;
    public GameObject canvas;
    public bool powerupBpos1 = false;
    public bool powerupBpos2 = false;
    public bool powerupBpos3 = false;


    public Joystick joystick;

    public GameObject healthBar;
    public GameManager gameManager;
    public Animator gameplayeUIAnimator;
    public InstantiateBullet instantiateBullet;
    public FacePlayer facePlayer;

    public int powerupsPickupCount = 0;

    // Update is called once per frame
    void Update()
    {
        HP = (int)healthBar.GetComponent<Slider>().value;

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
                    GameObject.Find("AudioM").GetComponent<AudioSource>().Pause();
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
                    GameObject.Find("AudioM").GetComponent<AudioSource>().Pause();
                }
            }
            else
            {
                GetComponent<ShakePlayer>().ShakeIt(0.3f);
                healthBar.GetComponent<Shake>().ShakeIt(0.5f);
                healthBar.GetComponent<HealthBarScript>().SetHealth(HP - 1);
                GetComponent<Animator>().SetBool("PlayerHit", true);
            }
        }

        if (powerupsPickupCount < 3)
        {
            if (trig.gameObject.name == "heart(Clone)")
            {
                powerupsPickupCount++;
                if (powerupBpos1 && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject heartB = Instantiate(powerupsBs[0], Vector2.zero, Quaternion.identity);
                    heartB.transform.SetParent(canvas.transform);
                    heartB.transform.localScale = Vector3.one;
                    heartB.transform.position = new Vector2(325f, 500f);
                    powerupBpos3 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject heartB = Instantiate(powerupsBs[0], Vector2.zero, Quaternion.identity);
                    heartB.transform.SetParent(canvas.transform);
                    heartB.transform.localScale = Vector3.one;
                    heartB.transform.position = new Vector2(450f, 250f);
                    powerupBpos2 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject heartB = Instantiate(powerupsBs[0], Vector2.zero, Quaternion.identity);
                    heartB.transform.SetParent(canvas.transform);
                    heartB.transform.localScale = Vector3.one;
                    heartB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject heartB = Instantiate(powerupsBs[0], Vector2.zero, Quaternion.identity);
                    heartB.transform.SetParent(canvas.transform);
                    heartB.transform.localScale = Vector3.one;
                    heartB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject heartB = Instantiate(powerupsBs[0], Vector2.zero, Quaternion.identity);
                    heartB.transform.SetParent(canvas.transform);
                    heartB.transform.localScale = Vector3.one;
                    heartB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3)
                {
                    GameObject heartB = Instantiate(powerupsBs[0], Vector2.zero, Quaternion.identity);
                    heartB.transform.SetParent(canvas.transform);
                    heartB.transform.localScale = Vector3.one;
                    heartB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject heartB = Instantiate(powerupsBs[0], Vector2.zero, Quaternion.identity);
                    heartB.transform.SetParent(canvas.transform);
                    heartB.transform.localScale = Vector3.one;
                    heartB.transform.position = new Vector2(450f, 250f);
                    powerupBpos2 = true;
                }

                Destroy(trig.gameObject);
            }

            if (trig.gameObject.name == "shield(Clone)")
            {
                powerupsPickupCount++;
                if (powerupBpos1 && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1], Vector2.zero, Quaternion.identity);
                    shieldB.transform.SetParent(canvas.transform);
                    shieldB.transform.localScale = Vector3.one;
                    shieldB.transform.position = new Vector2(325f, 500f);
                    powerupBpos3 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1], Vector2.zero, Quaternion.identity);
                    shieldB.transform.SetParent(canvas.transform);
                    shieldB.transform.localScale = Vector3.one;
                    shieldB.transform.position = new Vector2(450f, 250f);
                    powerupBpos2 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1], Vector2.zero, Quaternion.identity);
                    shieldB.transform.SetParent(canvas.transform);
                    shieldB.transform.localScale = Vector3.one;
                    shieldB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1], Vector2.zero, Quaternion.identity);
                    shieldB.transform.SetParent(canvas.transform);
                    shieldB.transform.localScale = Vector3.one;
                    shieldB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1], Vector2.zero, Quaternion.identity);
                    shieldB.transform.SetParent(canvas.transform);
                    shieldB.transform.localScale = Vector3.one;
                    shieldB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1], Vector2.zero, Quaternion.identity);
                    shieldB.transform.SetParent(canvas.transform);
                    shieldB.transform.localScale = Vector3.one;
                    shieldB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject shieldB = Instantiate(powerupsBs[1], Vector2.zero, Quaternion.identity);
                    shieldB.transform.SetParent(canvas.transform);
                    shieldB.transform.localScale = Vector3.one;
                    shieldB.transform.position = new Vector2(450f, 250f);
                    powerupBpos2 = true;
                }

                Destroy(trig.gameObject);
            }

            if (trig.gameObject.name == "fire(Clone)")
            {
                powerupsPickupCount++;
                if (powerupBpos1 && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject fireB = Instantiate(powerupsBs[2], Vector2.zero, Quaternion.identity);
                    fireB.transform.SetParent(canvas.transform);
                    fireB.transform.localScale = Vector3.one;
                    fireB.transform.position = new Vector2(325f, 500f);
                    powerupBpos3 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject fireB = Instantiate(powerupsBs[2], Vector2.zero, Quaternion.identity);
                    fireB.transform.SetParent(canvas.transform);
                    fireB.transform.localScale = Vector3.one;
                    fireB.transform.position = new Vector2(450f, 250f);
                    powerupBpos2 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3 == false)
                {
                    GameObject fireB = Instantiate(powerupsBs[2], Vector2.zero, Quaternion.identity);
                    fireB.transform.SetParent(canvas.transform);
                    fireB.transform.localScale = Vector3.one;
                    fireB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject fireB = Instantiate(powerupsBs[2], Vector2.zero, Quaternion.identity);
                    fireB.transform.SetParent(canvas.transform);
                    fireB.transform.localScale = Vector3.one;
                    fireB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3 == false)
                {
                    GameObject fireB = Instantiate(powerupsBs[2], Vector2.zero, Quaternion.identity);
                    fireB.transform.SetParent(canvas.transform);
                    fireB.transform.localScale = Vector3.one;
                    fireB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 == false && powerupBpos2 && powerupBpos3)
                {
                    GameObject fireB = Instantiate(powerupsBs[2], Vector2.zero, Quaternion.identity);
                    fireB.transform.SetParent(canvas.transform);
                    fireB.transform.localScale = Vector3.one;
                    fireB.transform.position = new Vector2(200f, 250f);
                    powerupBpos1 = true;
                }
                if (powerupBpos1 && powerupBpos2 == false && powerupBpos3)
                {
                    GameObject fireB = Instantiate(powerupsBs[2], Vector2.zero, Quaternion.identity);
                    fireB.transform.SetParent(canvas.transform);
                    fireB.transform.localScale = Vector3.one;
                    fireB.transform.position = new Vector2(450f, 250f);
                    powerupBpos2 = true;
                }

                Destroy(trig.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D trigOut)
    {
        if (trigOut.gameObject.CompareTag("Bullet")) GetComponent<Animator>().SetBool("PlayerHit", false);
    }

    void PlayerRevived() => GetComponent<Animator>().SetBool("Reviving", false);
}
