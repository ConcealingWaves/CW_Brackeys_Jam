using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Material playerBulletMat;
    [SerializeField] private Material enemyBulletMat;

    [HideInInspector] public Collider2D col;

    private ObjectPool myPool;
    private string myPoolTag;
    private static SpriteRenderer playBounds;
    
    [SerializeField] private float baseSpeed;
    private float speed;
    [SerializeField] private float damage;

    private SpriteRenderer mySprite;
    private TrailRenderer myTrail;

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
        myTrail = GetComponentInChildren<TrailRenderer>();
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
        ResetTrail();
    }

    private void FixedUpdate()
    {
        mySprite.material = gameObject.layer != LayerMask.NameToLayer("Friendly") ? enemyBulletMat : playerBulletMat;
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector2(0,1) * (speed * Time.fixedDeltaTime));
        if (Mathf.Abs(playBounds.bounds.center.x - transform.position.x) > playBounds.bounds.extents.x ||
            Mathf.Abs(playBounds.bounds.center.y - transform.position.y) > playBounds.bounds.extents.y)
        {
            DespawnMe();
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
        if (myPool == null) return;
        MyPool.Despawn(gameObject, MyPoolTag);
    }
    
    public void Rotate(float r)
    {
        transform.Rotate(transform.forward, r);
    }

    public void ResetTrail()
    {
        if (myTrail == null) return;
        myTrail.time = 0.1f;
        myTrail.Clear();
    }

}
