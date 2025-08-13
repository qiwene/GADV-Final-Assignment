using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Initialising of variables
    public string playerTag = "Player";
    private Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    private bool cameraMessage = false;

    void Start()
    {
        /*The code below is used to find the game object with the tag "Player" but if it
         is unable to find the game object, it sends out a message which is used to debug.*/

        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
            Application.Quit();
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            //
            this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
            if (cameraMessage == false)
            {
                Debug.Log("Camera is functioning.");
                cameraMessage = true;
            }
        }
    }
}