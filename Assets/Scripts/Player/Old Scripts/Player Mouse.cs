using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    public Vector3 direction;

    void Update()
    {
        // Get the mouse coordinate then subtract player position to get a vector 3 for direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        direction.z = 0f;

        // Calculate the angle between the player and the mouse position in radian then change to deg using Mathf.Rad2Deg
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
}