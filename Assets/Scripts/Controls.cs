using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public int HP;

    public Joystick joystick;

    public GameObject healthBar;
    public GameManager gameManager;
    public Animator gameplayeUIAnimator;
    public InstantiateBullet instantiateBullet;
    public FacePlayer facePlayer;

    private void Start()
    {
        HP = (int)healthBar.GetComponent<Slider>().value;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.revived) Invoke("PlayerRevived", 1f);

        GetComponent<Rigidbody2D>().velocity = new Vector2(joystick.Horizontal, joystick.Vertical) / (Time.timeScale == 0 ? 1 : Time.timeScale) * 2.5f;

        if (Input.touchCount > 0)
            if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) return;
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Bullet"))
        {
            HP -= 1;
            if (HP < 1)
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
                GetComponent<ShakePlayer>().ShakeIt(0.3f);
                healthBar.GetComponent<Shake>().ShakeIt(0.5f);
                healthBar.GetComponent<HealthBarScript>().SetHealth(HP - 1);
                GetComponent<Animator>().SetBool("PlayerHit", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D trigOut)
    {
        if (trigOut.gameObject.CompareTag("Bullet")) GetComponent<Animator>().SetBool("PlayerHit", false);
    }

    void PlayerRevived() => GetComponent<Animator>().SetBool("Reviving", false);
}
