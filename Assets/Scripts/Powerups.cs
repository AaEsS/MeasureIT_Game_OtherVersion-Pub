using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Powerups : MonoBehaviour
{
    public GameObject[] powerupsList;
    float[] timesOfSpawn;
    float timeBetweenSpawns;

    public AudioClip healSound;

    public Sprite meShielded;
    
    public Transform playerTransform;
    public SpriteRenderer playerSprite;
    public CapsuleCollider2D playerCapsuleColl;
    public CircleCollider2D playerCircleColl;
    public Controls playerControls;
    public Animator playerAnimator;

    Vector2 randomPos;

    public Transform leftWall, righWall;

    void Start()
    {
        timesOfSpawn = new float[4] {1f, 3f, 5f, 8f};
        timeBetweenSpawns = timesOfSpawn[Random.Range(0, timesOfSpawn.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenSpawns -= Time.deltaTime;
        if (timeBetweenSpawns <= 0f)
        {
            SpawnPowerup();
        }
    }

    void SpawnPowerup()
    {
        timeBetweenSpawns = timesOfSpawn[Random.Range(0, timesOfSpawn.Length)];
        randomPos = new Vector2(Random.Range(leftWall.position.x + 1.7f, righWall.position.x - 1.7f), Random.Range(-4.577f, 4.577f));
        Instantiate(powerupsList[Random.Range(0, powerupsList.Length)], randomPos, Quaternion.identity);
    }

    public void ActivatePowerup()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "HeartPowerup(Clone)" && playerControls.healthBar.GetComponent<Slider>().value < 5)
        {
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[0].transform.position) < 1f) playerControls.powerupBpos1 = false;
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[1].transform.position) < 1f) playerControls.powerupBpos2 = false;
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[2].transform.position) < 1f) playerControls.powerupBpos3 = false;

            playerControls.healthBar.GetComponent<Slider>().value++;
            AudioSource.PlayClipAtPoint(healSound, playerTransform.position);
            playerControls.powerupsPickupCount--;

            Destroy(EventSystem.current.currentSelectedGameObject);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "ShieldPowerup(Clone)" && playerControls.shielded == false && playerControls.onFire == false)
        {
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[0].transform.position) < 1f) playerControls.powerupBpos1 = false;
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[1].transform.position) < 1f) playerControls.powerupBpos2 = false;
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[2].transform.position) < 1f) playerControls.powerupBpos3 = false;

            ShieldOnMethod();
            playerControls.powerupsPickupCount--;

            Destroy(EventSystem.current.currentSelectedGameObject);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "FirePowerup(Clone)" && playerControls.onFire == false && playerControls.shielded == false)
        {
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[0].transform.position) < 1f) playerControls.powerupBpos1 = false;
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[1].transform.position) < 1f) playerControls.powerupBpos2 = false;
            if (Vector2.Distance(EventSystem.current.currentSelectedGameObject.transform.position, playerControls.powerupsBs[2].transform.position) < 1f) playerControls.powerupBpos3 = false;

            FireOnMethod();
            playerControls.powerupsPickupCount--;

            Destroy(EventSystem.current.currentSelectedGameObject);
        }
    }

    void FireOnMethod()
    {
        playerControls.particleFireEffect.SetActive(true);
        playerControls.onFire = true;
        playerControls.firePTime = 5f;
    }

    void ShieldOnMethod()
    {
        playerCapsuleColl.enabled = false;
        playerCircleColl.enabled = true;
        playerSprite.sprite = meShielded;
        playerControls.shielded = true;
        playerControls.shieldPTime = 5f;
        playerAnimator.enabled = false;
    }
}
