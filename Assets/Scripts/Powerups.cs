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

    public GameObject player;

    Vector2 randomPos;

    public Transform leftWall, righWall;

    void Start()
    {
        //timesOfSpawn = new float[5] {3f, 5f, 10f, 15f, 20f};
        timesOfSpawn = new float[2] {1f, 3f};
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
        if (EventSystem.current.currentSelectedGameObject.name == "HeartPowerup(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 199f && EventSystem.current.currentSelectedGameObject.transform.position.x < 201f) player.GetComponent<Controls>().powerupBpos1 = false;
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 449f && EventSystem.current.currentSelectedGameObject.transform.position.x < 451f) player.GetComponent<Controls>().powerupBpos2 = false;
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 324f && EventSystem.current.currentSelectedGameObject.transform.position.x < 326f) player.GetComponent<Controls>().powerupBpos3 = false;

            player.GetComponent<Controls>().healthBar.GetComponent<Slider>().value++;

            Destroy(EventSystem.current.currentSelectedGameObject);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "ShieldPowerup(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 199f && EventSystem.current.currentSelectedGameObject.transform.position.x < 201f) player.GetComponent<Controls>().powerupBpos1 = false;
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 449f && EventSystem.current.currentSelectedGameObject.transform.position.x < 451f) player.GetComponent<Controls>().powerupBpos2 = false;
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 324f && EventSystem.current.currentSelectedGameObject.transform.position.x < 326f) player.GetComponent<Controls>().powerupBpos3 = false;

            Destroy(EventSystem.current.currentSelectedGameObject);
        }

        if (EventSystem.current.currentSelectedGameObject.name == "FirePowerup(Clone)")
        {
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 199f && EventSystem.current.currentSelectedGameObject.transform.position.x < 201f) player.GetComponent<Controls>().powerupBpos1 = false;
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 449f && EventSystem.current.currentSelectedGameObject.transform.position.x < 451f) player.GetComponent<Controls>().powerupBpos2 = false;
            if (EventSystem.current.currentSelectedGameObject.transform.position.x > 324f && EventSystem.current.currentSelectedGameObject.transform.position.x < 326f) player.GetComponent<Controls>().powerupBpos3 = false;

            Destroy(EventSystem.current.currentSelectedGameObject);
        }

        player.GetComponent<Controls>().powerupsPickupCount--;
    }
}
