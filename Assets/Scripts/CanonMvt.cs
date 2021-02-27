using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanonMvt : MonoBehaviour
{
    private Vector3 startPos;

    public float frequency;
    public float magnitude;
    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        if (Mathf.Abs(Mathf.Cos(gameObject.transform.localEulerAngles.z * Mathf.Deg2Rad)) != 1 && magnitude != 0)
            magnitude = 2 * GameObject.Find("ForWalls2").transform.position.x - 6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (frequency != 0.0f) transform.position = startPos + transform.up * Mathf.Sin(Time.time * frequency + offset) * magnitude;
        else transform.position = transform.position;
    }   
}