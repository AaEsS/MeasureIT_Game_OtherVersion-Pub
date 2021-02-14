using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChooseSF : MonoBehaviour
{
    public GameObject timer, collectables;
    public PauseMenuS pauseActive;

    AudioSource slowMoOutSoundPlayer;
    public AudioClip slowMoOutSound;
    public AudioClip collectableSound;

    AudioSource intenseMusic;

    bool musicPlayedOnce = true;

    private float deltaX, deltaY;
    private Rigidbody2D rb;

    bool backToNormalTime = false;

    public Animator SFAnims;
    public Animator instructions;

    public int collectableCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        slowMoOutSoundPlayer = gameObject.AddComponent<AudioSource>();
        intenseMusic = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (backToNormalTime == true && Time.timeScale != 0f)
        {
            Time.timeScale += 0.01f;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }

        if (Input.touchCount > 0)
        {
            if (SceneManager.GetActiveScene().name == "1" && PlayerPrefs.GetInt("lvlReached") < 2) instructions.SetTrigger("DragInstruct");

            collectables.SetActive(true);
            SFAnims.SetTrigger("SZOut");
            backToNormalTime = true;
            timer.SetActive(true);
            pauseActive.enabled = true;
            if (musicPlayedOnce == true)
            {
                slowMoOutSoundPlayer.PlayOneShot(slowMoOutSound);
                intenseMusic.PlayDelayed(1f);
                musicPlayedOnce = false;
            }
            
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    break;

                case TouchPhase.Moved:
                    rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    break;

                case TouchPhase.Ended:
                    rb.velocity = Vector2.zero;
                    break;
            }
        }

        if (GameObject.Find("Timer") != null && GameObject.Find("Timer").GetComponent<TimerWin>().howMuchTime < 0f)
            intenseMusic.Stop();
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Collectable"))
        {
            AudioSource.PlayClipAtPoint(collectableSound, transform.position);
            Destroy(trig.gameObject);
            collectableCount++;
        }
    }
}