using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pogohitbox : MonoBehaviour
{
    public PolygonCollider2D bottomHitbox;

    void Start()
    {
        bottomHitbox = GetComponent<PolygonCollider2D>();
    }
}
