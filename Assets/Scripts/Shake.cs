using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [Range(0f, 2f)]
    public float intensity;
    Transform target;
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Transform>();
        initialPos = target.localPosition;
    }

    float shakeDur = 0f;

    public void ShakeIt(float dur)
    {
        if (dur > 0) shakeDur += dur;
    }

    bool isShaking = false;

    // Update is called once per frame
    void Update()
    {
        if (shakeDur > 0 && !isShaking) StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        isShaking = true;

        var starTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < starTime + shakeDur)
        {
            var randomPoint = new Vector3(Random.Range(-1f, 1f) * intensity, Random.Range(-1f, 1f) * intensity, initialPos.z);
            target.localPosition = randomPoint;
            yield return null;
        }

        shakeDur = 0f;
        target.localPosition = initialPos;
        isShaking = false;
    }
}
