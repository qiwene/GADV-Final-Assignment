using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public float pogoBounceForce = 12.0f;
    private Vector3 direction3D;
    private Vector2 normalisedDirection;
    private Rigidbody2D rb2d;
    private CapsuleCollider2D playerHitbox;
    private PolygonCollider2D bottomHitbox;
    private PlayerMouse playerMouseScript;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerHitbox = GetComponent<CapsuleCollider2D>();
        bottomHitbox = transform.Find("Bottom hitbox").GetComponent<PolygonCollider2D>();
        playerMouseScript = GetComponent<PlayerMouse>();

        if (playerHitbox == null)
        {
            Debug.LogError("Player Hitbox Polygon Collider 2D not found!");
        }
        else
        {
            Debug.Log("Found hitbox: " + playerHitbox.name);
        }

        if (bottomHitbox == null)
        {
            Debug.LogError("Var bottomHitbox not found!");
        }
        else
        {
            Debug.Log("Found hitbox: " + bottomHitbox.name);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided object has tag "Floor"
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Check if the collider involved in this collision is the bottomHitbox collider
            Debug.Log("In contact with floor");
            if (collision.otherCollider == bottomHitbox)
            {
                direction3D = playerMouseScript.direction;
                Vector2 direction2D = new (direction3D.x , direction3D.y);
                Debug.Log("Direction: " + direction2D);
                normalisedDirection = direction2D.normalized;
                Debug.Log("Normalised Direction: " + normalisedDirection);
                rb2d.velocity = normalisedDirection * pogoBounceForce;
                Debug.Log("Velocity" + rb2d.velocity);
            }
        }
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided object has tag "Floor"
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Check if the collider involved in this collision is the bottomHitbox collider
            Debug.Log("In contact with floor");
            if (collision.otherCollider == bottomHitbox)
            {
                direction3D = playerMouseScript.direction;
                Vector2 direction2D = new(direction3D.x, direction3D.y);
                Debug.Log("Direction: " + direction2D);
                normalisedDirection = direction2D.normalized;
                Debug.Log("Normalised Direction: " + normalisedDirection);
                rb2d.velocity = normalisedDirection * pogoBounceForce;
                Debug.Log("Velocity" + rb2d.velocity);
            }
        }
    }*/
}
