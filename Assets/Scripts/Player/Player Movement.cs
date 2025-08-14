using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PlayerMovement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public float pogoBounceForce = 12.0f;
    public float maxSpeed = 12.0f;
    private float angle = 0;
    private Vector3 direction3D;
    private Vector2 normalisedDirection;
    public Vector3 originalScale;
    public Vector3 targetScale;
    public float animDurationA = 0.1f;
    public float animDurationB = 0.1f;
    private bool rightClickChecker;
    private bool isBouncing = false;

    // Bounce settings
    public enum BounceEffects
    {
        None,
        Effect1,
        Effect2
    }
    public BounceEffects currentEffect;

    // Components
    private Rigidbody2D rb2d;
    private CapsuleCollider2D playerHitbox;
    private PolygonCollider2D bottomHitbox;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = new Vector3(0.8f, 0.3f, transform.localScale.z);

        rb2d = GetComponent<Rigidbody2D>();
        playerHitbox = GetComponent<CapsuleCollider2D>();
        bottomHitbox = transform.Find("Bottom hitbox").GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Toggle for right clicks
        if (Input.GetMouseButtonUp(1))
        {
            rightClickChecker = !rightClickChecker;
        }

        // Get the mouse coordinate then subtract player position to get a vector 3 for direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction3D = mousePos - transform.position;
        direction3D.z = 0f;

        // Calculate the angle between the player and the mouse position in radian then change to deg using Mathf.Rad2Deg
        angle = Mathf.Atan2(direction3D.y, direction3D.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        //Debug.Log("Angle: " + angle);

        //If toggle turned off mouse controls players, if toggled turned on the player ragdolls
        if (rightClickChecker == false)
        {
            if ((angle >= -90) && (angle <= 90))
            {
                spriteRenderer.flipX = false;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
            else
            {
                spriteRenderer.flipX = true;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
        }
    }

    //-----------------------Bounce Physics and mouse movement---------------------------------//

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (currentEffect == BounceEffects.None)
            {

                Debug.Log("In contact with floor");
                if (collision.otherCollider == bottomHitbox)
                {
                    // Calculates the direction, normalises it and then makes it bounce while clamping the value
                    rb2d.AddForce((transform.up * pogoBounceForce), ForceMode2D.Impulse);
                    Debug.Log("Force: " + (normalisedDirection * pogoBounceForce));

                    rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
                }
            }
            else if (currentEffect == BounceEffects.Effect1)
            {
                Debug.Log("In contact with floor");
                if (collision.otherCollider == bottomHitbox)
                {
                    StartCoroutine(BounceEffect1());
                    // Calculates the direction, normalises it and then makes it bounce while clamping the value
                    rb2d.AddForce((transform.up * pogoBounceForce), ForceMode2D.Impulse);
                    Debug.Log("Force: " + (normalisedDirection * pogoBounceForce));

                    rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
                }
            }
            else if (currentEffect == BounceEffects.Effect2)
            {
                if (isBouncing) return;
                if (collision.gameObject.CompareTag("Floor") && collision.otherCollider == bottomHitbox)
                {
                    StartCoroutine(BounceEffect2Sequence());
                }
            }
        }
    }

    // Old Bounce code
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if collided object has tag "Floor" and if the collision involves bottomHitbox
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("In contact with floor");
            if (collision.otherCollider == bottomHitbox)
            {
                // Bounce visual effect part A (shrinks a bit)
                StartCoroutine(BounceEffect_PartA());

                // Calculates the direction, normalises it and then makes it bounce while clamping the value
                Vector2 direction2D = new(direction3D.x, direction3D.y);
                normalisedDirection = direction2D.normalized;
                rb2d.AddForce((normalisedDirection * pogoBounceForce), ForceMode2D.Impulse);
                Debug.Log("Force: " + (normalisedDirection * pogoBounceForce));

                rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);

                // Bounce visual effect part B (goes back to normal scale)
                StartCoroutine(BounceEffect_PartB());
            }
        }
    }*/



    //---------------------Bounce effects-----------------------//


    // Bounce effect 1 method
    private IEnumerator BounceEffect1()
    {
        float t = 0;

        // Animating from original scale to target scale
        while (t < animDurationA)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t / animDurationA);
            yield return null;
        }
        t = 0;
        while (t < animDurationB)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t / animDurationB);
            yield return null;
        }
    }


    // Bounce effect 2 methods
    private IEnumerator BounceEffect2Sequence()
    {
        isBouncing = true;
        // Part A: shrink animation
        yield return StartCoroutine(BounceEffect2_PartA());

        // Physics bounce (after animation completes)
        rb2d.AddForce((transform.up * pogoBounceForce), ForceMode2D.Impulse);
        rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
        Debug.Log("Force: " + (normalisedDirection * pogoBounceForce));

        // Part B: expand animation back to normal
        yield return StartCoroutine(BounceEffect2_PartB());
        isBouncing = false;
    }


    private IEnumerator BounceEffect2_PartA()
    {
        float t = 0;

        // Animating from original scale to target scale
        while (t < animDurationA)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t / animDurationA);
            yield return null;
        }
    }

    private IEnumerator BounceEffect2_PartB()
    {
        float t = 0;

        // Animating from target scale to the original scale
        t = 0;
        while (t < animDurationB)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t / animDurationB);
            yield return null;
        }
    }
}