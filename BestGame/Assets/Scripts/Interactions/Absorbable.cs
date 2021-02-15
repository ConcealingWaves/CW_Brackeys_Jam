using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Absorbable : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isAbsorbed;
    
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
        Destroy(rb);
        spriteRenderer.sprite = absorbedSprite;
        killMe.enabled = false;
        isAbsorbed = true;
    }

    public void Breakaway()
    {
        gameObject.layer = LayerMask.NameToLayer(originalLayer);
        rb = gameObject.AddComponent<Rigidbody2D>();
        SetRBValues(rb);
        spriteRenderer.sprite = originalSprite;
        killMe.enabled = true;
        isAbsorbed = false;
    }

    private void SetRBValues(Rigidbody2D r)
    {
        r.gravityScale = 0;
    }
}
