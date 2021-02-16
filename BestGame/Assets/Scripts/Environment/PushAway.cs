using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PushAway : MonoBehaviour
{
    private Collider2D col;
    [SerializeField] private float relVertPush;
    [SerializeField] private float relHorzPush;
    [SerializeField] private float intensity;
    private Vector2 pushVector;
    private Vector2 currentBuiltVector;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        pushVector = new Vector2(relHorzPush, relVertPush).normalized;
        currentBuiltVector = new Vector2(0, 0);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        EntityController cont = other.gameObject.GetComponent<EntityController>();
        if (cont != null && cont.AllowedToMove)
        {
            Vector2 toAdd = pushVector * intensity * Time.fixedDeltaTime;
            cont.ExternalMoveVector += toAdd;
            currentBuiltVector += toAdd;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EntityController cont = other.gameObject.GetComponent<EntityController>();
        if (cont != null && cont.AllowedToMove)
        {
            cont.ExternalMoveVector -= currentBuiltVector;
            currentBuiltVector = Vector2.zero;
        }
    }
}
