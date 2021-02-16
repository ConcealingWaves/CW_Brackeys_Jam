using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Collider2D))]
public class Absorbable : AbsorbBase
{
    private const float SMALL_MAGNITUDE = 0.3f;
    
    private Absorber primaryAbsorber;
    private AbsorbBase secondaryAbsorber;
    
    private Rigidbody2D rb;
    [HideInInspector] public Collider2D col;
    public override bool IsAbsorbed() => primaryAbsorber != null;
    
    private Sprite originalSprite;
    private string originalLayer;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite absorbedSprite;
    [SerializeField] private Enemy killMe;

    private EntityController cont;

    public Absorber PrimaryAbsorber
    {
        get => primaryAbsorber;
    }

    public AbsorbBase SecondaryAbsorber
    {
        get => secondaryAbsorber;
        set => secondaryAbsorber = value;
    }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        cont = GetComponent<EntityController>();
        SetRBValues(rb);
        originalLayer = LayerMask.LayerToName(gameObject.layer);
        originalSprite = spriteRenderer.sprite;
    }

    protected override void Update()
    {
        base.Update();
        if(secondaryAbsorber!=null && secondaryAbsorber.isActiveAndEnabled == false)
            Breakaway();
        if(rb.velocity.magnitude >= SMALL_MAGNITUDE)
            Breakaway();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
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
        cont.AllowedToMove = false;
    }

    public void Breakaway()
    {
        gameObject.layer = LayerMask.NameToLayer(originalLayer);
        transform.SetParent(null);
        primaryAbsorber = null;
        if (secondaryAbsorber != null)
        {
            secondaryAbsorber.OnDetach -= Breakaway;
            secondaryAbsorber = null;
        }
        RaiseDetachEvent();
        if(cont != null)
            cont.AllowedToMove = true;
        enabled = false;
    }

    private void SetRBValues(Rigidbody2D r)
    {
        if(r!=null) 
            r.gravityScale = 0;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Absorbable absorbable = other.gameObject.GetComponent<Absorbable>();
        if (IsAbsorbed() && absorbable != null)
        {
            primaryAbsorber.AddToAbsorbedUnits(absorbable);
            absorbable.SecondaryAbsorber = this;
            absorbable.OnDetach += Breakaway;
        }
    }
    
    
}
