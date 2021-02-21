using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [HideInInspector] public Collider2D col;

    private ObjectPool myPool;
    private string myPoolTag;
    private static SpriteRenderer playBounds;
    
    [SerializeField] private float baseSpeed;
    private float speed;
    [SerializeField] private float damage;

    private SpriteRenderer mySprite;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public ObjectPool MyPool
    {
        private get => myPool;
        set => myPool = value;
    }

    public string MyPoolTag
    {
        private get => myPoolTag;
        set => myPoolTag = value;
    }

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (playBounds == null)
        {
            playBounds = GameObject.FindWithTag("Stage").GetComponent<SpriteRenderer>();
        }
    }

    private void OnEnable()
    {
        speed = baseSpeed;
        mySprite.color = gameObject.layer != LayerMask.NameToLayer("Friendly") ? new Color(1,0,0,1) : new Color(1,1,1,0.7f);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector2(0,1) * (speed * Time.fixedDeltaTime));
        if (Mathf.Abs(playBounds.bounds.center.x - transform.position.x) > playBounds.bounds.extents.x ||
            Mathf.Abs(playBounds.bounds.center.y - transform.position.y) > playBounds.bounds.extents.y)
        {
            myPool.Despawn(gameObject, myPoolTag);
        }
    }

  
    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damaged = other.gameObject.GetComponent<IDamageable>();

        damaged?.TakeHit(damage);
        DespawnMe();
    }

    private void DespawnMe()
    {
        MyPool.Despawn(gameObject, MyPoolTag);
    }
    
    public void Rotate(float r)
    {
        transform.Rotate(transform.forward, r);
    }

}
