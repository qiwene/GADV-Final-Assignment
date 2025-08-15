using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Endlevel : MonoBehaviour
{
    public bool gameComplete = false;
    public bool gameOver = false;
    private PolygonCollider2D hitbox;
    private UiManager uiManager;

    private void Start()
    {
        hitbox = GetComponent<PolygonCollider2D>();
        GameObject uiObject = GameObject.FindWithTag("UI");
        uiManager = uiObject.GetComponent<UiManager>();
        uiManager.HideUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameComplete) return;
        if (other.gameObject.CompareTag("Player"))
        {
            gameComplete = true;
            uiManager.ShowUI();
        }
    }
}
