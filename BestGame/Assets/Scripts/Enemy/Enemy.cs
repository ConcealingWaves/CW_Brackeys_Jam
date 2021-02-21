using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    public Projectile projectile;
    [HideInInspector]
    public Collider2D col;
    private ObjectPool pool;
    public float moveSpeed=5.0f;    //The move speed of the enemy
    public float fireInterval = 0.2f;
    public float bulletSpeed = 10.0f;
    [HideInInspector]
    public Vector2 TopDestroyBoundary;
    [HideInInspector]
    public Vector2 BottomDestroyBoundary;
    [HideInInspector]
    public Vector2 LeftDestroyBoundary;
    [HideInInspector]
    public Vector2 RightDestroyBoundary;

    [SerializeField] private float value = 100;

    private EntityController cont;

    public float Value => value;

    private bool isPause = false;

    public bool bounded = false;
    void Awake() {
        //InvokeRepeating("fire", 0 ,fireInterval);
        col = GetComponent<Collider2D>();
        cont = GetComponent<EntityController>();
    }

    private void OnEnable()
    {
        StartCoroutine(ShootLeniencySequence());
    }

    private void Start()
    {
        pool = FindObjectOfType<ObjectPool>();
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if((pos.x<LeftDestroyBoundary.x||pos.x>RightDestroyBoundary.x
        ||pos.y<BottomDestroyBoundary.y||pos.y>TopDestroyBoundary.y)&&bounded)
        {
            GameObject.Destroy(gameObject);
        }
    }

    IEnumerator ShootLeniencySequence()
    {
        cont.AllowedToShoot = false;
        yield return new WaitForSeconds(1.6f);
        cont.AllowedToShoot = true;
    }
    public void GamePause()
    {
        isPause = true;
        //CancelInvoke();

    }
    public void GameResume()
    {
        isPause =false;
        //InvokeRepeating("fire", 0 ,fireInterval);
    }

    void move()
    {
        Transform t = transform;
        transform.Translate(new Vector2(0,1) * (moveSpeed * Time.deltaTime));
        
    }

    void fire()
    {
        Projectile toSpawn = pool.Spawn(projectile.gameObject, projectile.name, transform.position, transform.rotation).GetComponent<Projectile>();
        Physics2D.IgnoreCollision(toSpawn.col, col);
        toSpawn.gameObject.layer = gameObject.layer;
        toSpawn.MyPool = pool;
        toSpawn.MyPoolTag = projectile.name;
        toSpawn.Speed += moveSpeed;
    }
}
