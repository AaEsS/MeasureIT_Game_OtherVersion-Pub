using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        FacingPlayer();
    }

    void FacingPlayer()
    {
        Vector3 mousePosition = target.position;
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.right = direction;
    }
}
