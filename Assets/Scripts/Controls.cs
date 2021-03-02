using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public int HP;
    bool hitNotDead = false;
    float damageEffectTime = 0f;

    public Sprite jzScared;
    public Sprite jz;

    public Joystick joystick;

    private void Start()
    {
        HP = (int)GameObject.Find("HealthBar").GetComponent<Slider>().value;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(joystick.Horizontal, joystick.Vertical) / (Time.timeScale == 0 ? 1 : Time.timeScale) * 2.5f;

        if (Input.touchCount > 0)
            if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) return;

        if (damageEffectTime > 2f)
        {
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
            GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = jz;
            hitNotDead = false;
        }
        if (hitNotDead == true) damageEffectTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Bullet"))
        {
            if (HP <= 1)
            {
                Time.timeScale = 1f;
                GetComponent<SpriteRenderer>().enabled = false;
                Destroy(GameObject.Find("ShooterPivot").GetComponent<FacePlayer>());
                Destroy(GameObject.Find("Shooter").GetComponent<InstantiateBullet>());
                foreach (var bllet in GameObject.FindGameObjectsWithTag("Bullet")) Destroy(bllet);
                GameObject.Find("GameplayUI").GetComponent<Animator>().SetTrigger("AfterDeathTrig");
                GameObject.Find("HealthBar").GetComponent<Shake>().ShakeIt(0.8f);
                GameObject.Find("HealthBar").GetComponent<HealthBarScript>().SetHealth(HP - 1);
                GameObject.Find("AudioM").GetComponent<AudioSource>().Stop();
            }
            else
            {
                GetComponent<ShakePlayer>().ShakeIt(0.3f);
                GameObject.Find("HealthBar").GetComponent<Shake>().ShakeIt(0.5f);
                GameObject.Find("HealthBar").GetComponent<HealthBarScript>().SetHealth(HP - 1);
                HP -= 1;
                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.7f);
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = jzScared;
                damageEffectTime = 0f;
                hitNotDead = true;
            }
        }
    }
}
