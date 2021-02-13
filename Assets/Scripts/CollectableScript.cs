using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    Vector3 startPos;
    float frequency = 10f;
    float magnitude = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Invoke("BecomeInteractable", 0.4f);
    }

    void BecomeInteractable() => GetComponent<EdgeCollider2D>().enabled = true;

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
