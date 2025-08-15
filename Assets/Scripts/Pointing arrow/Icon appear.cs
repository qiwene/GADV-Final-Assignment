using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iconappear : MonoBehaviour
{
    public float targetAlpha = 0f;
    public float fadeSpeed = 2f;
    public float xValue1 = 0f;
    public float xValue2 = 0f;

    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color c = spriteRenderer.color;
        c.a = 0f;
        spriteRenderer.color = c;
    }

    void Update()
    {
        // Decide the target alpha based on player distance
        // x value where words appear
        if ((playerTransform.position.x >= xValue1) && (playerTransform.position.x <= xValue2))
            targetAlpha = 1f;
        // x value where they dissapear (just everything else essentially)
        else
            targetAlpha = 0f;

        // Smoothly move alpha toward target
        Color c = spriteRenderer.color;
        c.a = Mathf.MoveTowards(c.a, targetAlpha, fadeSpeed * Time.deltaTime);
        spriteRenderer.color = c;
    }
}