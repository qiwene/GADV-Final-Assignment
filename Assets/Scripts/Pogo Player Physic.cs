using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoPlayerPhysics : MonoBehaviour
{
    public float pogoBounceForce = 1.0f;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //get rigid body component
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && collision.contactCount > 0)
        {
            ContactPoint2D contact = collision.GetContact(0);
            if (contact.normal.y > 0.5f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, pogoBounceForce);
            }
        }
    }
}
