using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BulletMvt : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D blt;
    Vector3 dir;

    AudioSource wallHitSound;
    public AudioClip WHSound;
    public AudioClip DSound;
    GameObject[] bullets;

    void Start()
    {
        blt = GetComponent<Rigidbody2D>();
        blt.velocity = transform.right * moveSpeed;
        bullets = GameObject.FindGameObjectsWithTag("Bullet");

        wallHitSound = gameObject.AddComponent<AudioSource>();

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -GameObject.Find("ForWalls2").transform.position.x + 1.481f, GameObject.Find("ForWalls2").transform.position.x - 1.481f), Mathf.Clamp(transform.position.y, -GameObject.Find("Ceiling").transform.position.y + 1.475f, GameObject.Find("Ceiling").transform.position.y - 1.475f));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Single speed = dir.magnitude;
        Vector3 direction = Vector3.Reflect(dir.normalized, collision.contacts[0].normal);
        float angle = Mathf.Atan2(blt.velocity.y, blt.velocity.x) * Mathf.Rad2Deg;

        blt.velocity = direction * Mathf.Max(speed, 0f);
        wallHitSound.PlayOneShot(WHSound);

        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 1f;
            AudioSource.PlayClipAtPoint(DSound, transform.position);
            Destroy(GameObject.Find("Player"));
            Destroy(GameObject.Find("ShooterPivot").GetComponent<FacePlayer>());
            foreach (GameObject bullet in bullets)
                Destroy(bullet);
            GameObject.Find("GameMan").GetComponent<GameManS>().enabled = true;
            GameObject.Find("Canvas").GetComponent<ButtonS>().stopTimer = true;
        }
    }

    void Update()
    {
        dir = blt.velocity;
        if (GameObject.Find("Timer") != null && GameObject.Find("Timer").GetComponent<TimerWin>().howMuchTime < 0f)
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}