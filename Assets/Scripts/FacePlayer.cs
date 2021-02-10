﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform target;
    float fovCanonAngle;

    // Update is called once per frame
    void Update()
    {
        FacingPlayer();
        //if (gameObject.transform.localRotation.z < 0.5f && gameObject.transform.localRotation.z > -0.5f) FacingPlayer();
    }

    void FacingPlayer()
    {
        Vector3 mousePosition = target.position;
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.right = direction;
    }
}
