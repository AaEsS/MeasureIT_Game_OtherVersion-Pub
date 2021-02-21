﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class InstantiateBullet : MonoBehaviour
{
    public Rigidbody2D bullet;
    public Button shootB;
    InstantiateBullet inst;
    public GameObject SF;
    public ChooseSF clickSF;
    GameObject[] shooterPivots;
    GameObject[] shooters;
    GameObject[] canons;
    public PauseMenuS pauseMScript;

    AudioSource slowMoInSoundPlayer;
    public AudioClip slowMoInSound;

    // Start is called before the first frame update
    void Start()
    {
        Button shoot = shootB.GetComponent<Button>();
        shootB.onClick.AddListener(InstantiateBulletButton);
        inst = GetComponent<InstantiateBullet>();
        shooterPivots = GameObject.FindGameObjectsWithTag("ShooterPivot");
        shooters = GameObject.FindGameObjectsWithTag("Shooter");
        canons = GameObject.FindGameObjectsWithTag("Canon");

        slowMoInSoundPlayer = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "1" && PlayerPrefs.GetInt("lvlReached") < 2) GameObject.Find("Player").GetComponent<Controls>().instructions.SetTrigger("MoveInstruct");

            foreach (GameObject shooter in shooters)
                shooter.GetComponent<LineRenderer>().enabled = false;
            GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = true;
            Instantiate(bullet, transform.position, transform.rotation);
            DoSlowMo();
            inst.enabled = false;
            foreach (GameObject shooterPivot in shooterPivots)
                Destroy(shooterPivot.GetComponent<FacePlayer>());
            foreach (GameObject canon in canons)
                Destroy(canon.GetComponent<CanonMvt>());
            SF.SetActive(true);
            Destroy(GameObject.Find("Player").GetComponent<Controls>());
            Destroy(GameObject.Find("Player").GetComponent<DragMouseMove>());
            clickSF.enabled = true;
            pauseMScript.enabled = false;
            if (transform.parent.transform.parent.gameObject.name == "Canon") slowMoInSoundPlayer.PlayOneShot(slowMoInSound);
            GameObject.Find("AudioM").GetComponent<AudioSource>().Stop();
            Destroy(GameObject.Find("ReflectionsCnt"));
            Destroy(GameObject.Find("CanonMvtSpeed"));
            GameObject.Find("PauseB").transform.position += new Vector3(1000f, 1000f);
            Destroy(GameObject.Find("ShootB"));
        }
    }

    void InstantiateBulletButton()
    {
        if (SceneManager.GetActiveScene().name == "1" && PlayerPrefs.GetInt("lvlReached") < 2) GameObject.Find("Player").GetComponent<Controls>().instructions.SetTrigger("MoveInstruct");

        foreach (GameObject shooter in shooters)
            shooter.GetComponent<LineRenderer>().enabled = false;
        GameObject.Find("Player").GetComponent<BoxCollider2D>().enabled = true;
        Instantiate(bullet, transform.position, transform.rotation);
        DoSlowMo();
        inst.enabled = false;
        foreach (GameObject shooterPivot in shooterPivots)
            Destroy(shooterPivot.GetComponent<FacePlayer>());
        foreach (GameObject canon in canons)
            Destroy(canon.GetComponent<CanonMvt>());
        SF.SetActive(true);
        Destroy(GameObject.Find("Player").GetComponent<Controls>());
        Destroy(GameObject.Find("Player").GetComponent<DragMouseMove>());
        clickSF.enabled = true;
        pauseMScript.enabled = false;
        if (transform.parent.transform.parent.gameObject.name == "Canon") slowMoInSoundPlayer.PlayOneShot(slowMoInSound);
        GameObject.Find("AudioM").GetComponent<AudioSource>().Stop();
        Destroy(GameObject.Find("ReflectionsCnt"));
        Destroy(GameObject.Find("CanonMvtSpeed"));
        GameObject.Find("PauseB").transform.position += new Vector3(1000f, 1000f);
        Destroy(GameObject.Find("ShootB"));
    }

    void DoSlowMo()
    {
        Time.timeScale = 0.03f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
