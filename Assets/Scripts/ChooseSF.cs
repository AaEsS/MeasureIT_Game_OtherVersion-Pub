using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class ChooseSF : MonoBehaviour
{
    public GameObject SF;
    public GameObject SFOut;
    public GameObject timer;
    public PauseMenuS pauseActive;

    AudioSource slowMoOutSoundPlayer;
    public AudioClip slowMoOutSound;

    AudioSource intenseMusic;

    bool musicPlayedOnce = true;

    private float deltaX, deltaY;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        slowMoOutSoundPlayer = gameObject.AddComponent<AudioSource>();
        intenseMusic = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Time.timeScale = 1f;
            SF.SetActive(false);
            SFOut.SetActive(true);
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

        if (GameObject.Find("Timer").GetComponent<TimerWin>().howMuchTime < 0f)
            intenseMusic.Stop();
    }
}
