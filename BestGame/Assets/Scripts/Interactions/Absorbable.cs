using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Absorbable : MonoBehaviour
{
    private Absorber primaryAbsorber;
    
    private Rigidbody2D rb;
    public bool IsAbsorbed => primaryAbsorber != null;
    
    private Sprite originalSprite;
    private string originalLayer;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite absorbedSprite;
    [SerializeField] private Enemy killMe;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRBValues(rb);
        originalLayer = LayerMask.LayerToName(gameObject.layer);
        originalSprite = spriteRenderer.sprite;
    }

    public void GetAbsorbed(Absorber absorber)
    {
        gameObject.layer = absorber.gameObject.layer;
        rb.isKinematic = true;
        rb.useFullKinematicContacts = true;
        //Destroy(rb);
        spriteRenderer.sprite = absorbedSprite;
        killMe.enabled = false;
        primaryAbsorber = absorber;
    }

    public void Breakaway()
    {
        gameObject.layer = LayerMask.NameToLayer(originalLayer);
        rb.isKinematic = false;
        rb.useFullKinematicContacts = true;
        //rb = gameObject.AddComponent<Rigidbody2D>();
        SetRBValues(rb);
        spriteRenderer.sprite = originalSprite;
        killMe.enabled = true;
        primaryAbsorber = null;
    }

    private void SetRBValues(Rigidbody2D r)
    {
        if(r!=null) 
            r.gravityScale = 0;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Absorbable absorbable = other.gameObject.GetComponent<Absorbable>();
        if (IsAbsorbed && absorbable != null)
        {
            primaryAbsorber.AddToAbsorbedUnits(absorbable);
        }
    }
    
}
