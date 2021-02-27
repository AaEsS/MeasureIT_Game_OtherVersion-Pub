using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jystck : MonoBehaviour
{
    Transform player;
    float speed;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    Transform handle;
    Transform circle;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        circle = GameObject.Find("Circle").transform;
        handle = GameObject.Find("Handle").transform;
    }

    // Update is called once per frame
    void Update()
    {
        speed = 30f / Time.timeScale;

        if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            handle.transform.position = pointA;
            circle.transform.position = pointA;
            handle.GetComponent<SpriteRenderer>().enabled = true;
            circle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }

        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 0.1f);
            moveCharacter(direction);

            handle.transform.position = new Vector2(pointA.x + direction.x, pointA.y + direction.y);
        }
        else
        {
            handle.GetComponent<SpriteRenderer>().enabled = false;
            circle.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void moveCharacter(Vector2 direction)
    {
        if (player != null) player.Translate(direction * speed * Time.deltaTime);
    }
}