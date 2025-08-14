using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Initialising of variables
    public string playerTag = "Player";
    private Transform player;
    private bool cameraMessage = false;

    void Start()
    {
        // Find the player with tag and if fail, sends out a message

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
        // If player found, the cameras position is assigned to the player except x-axis
        if (player != null)
        {
            //
            this.transform.position = new Vector3(player.position.x, player.position.y, -10);
            if (cameraMessage == false)
            {
                Debug.Log("Camera is functioning.");
                cameraMessage = true;
            }
        }
    }
}