using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChooseSF : MonoBehaviour
{
    float spd = 30;
    Vector3 targetPos;
    bool isMoving = false;
    public GameObject SF;
    public GameObject SFOut;
    bool only1Click = true;
    public GameObject timer;
    public PauseMenuS pauseActive;

    AudioSource slowMoOutSoundPlayer;
    public AudioClip slowMoOutSound;

    AudioSource intenseMusic;

    void Start()
    {
        slowMoOutSoundPlayer = gameObject.AddComponent<AudioSource>();
        intenseMusic = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && only1Click == true)
        {
            Time.timeScale = 1f;
            SetTargetPos();
            SF.SetActive(false);
            SFOut.SetActive(true);
            only1Click = false;
            timer.SetActive(true);
            pauseActive.enabled = true;
            slowMoOutSoundPlayer.PlayOneShot(slowMoOutSound);
            intenseMusic.PlayDelayed(1f);
        }

        if (GameObject.Find("Timer").GetComponent<TimerWin>().howMuchTime < 0f)
            intenseMusic.Stop();

        if (isMoving) Move();
    }

    void SetTargetPos()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;
        isMoving = true;
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, spd * Time.deltaTime);
        if (transform.position == targetPos) isMoving = false;
    }
}
