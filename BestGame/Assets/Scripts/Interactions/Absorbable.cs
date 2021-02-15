using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Absorbable : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool absorbed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite absorbedSprite;
    [SerializeField] private Enemy killMe;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetAbsorbed(Absorber absorber)
    {
        gameObject.layer = absorber.gameObject.layer;
        Destroy(rb);
        spriteRenderer.sprite = absorbedSprite;
        killMe.enabled = false;
        absorbed = true;
    }
}
