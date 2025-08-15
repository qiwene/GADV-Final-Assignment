using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    private PolygonCollider2D bottomHitbox;

    private void Start()
    {
        bottomHitbox = transform.Find("Bottom hitbox").GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Plays an audio if the player collides with object with tag "Floor" and if within bottomHitbox
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (collision.otherCollider == bottomHitbox)
            {
                audioSource.Play();
            }
        }
    }
}