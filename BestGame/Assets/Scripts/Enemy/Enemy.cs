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
    public Transform destroyPos;  //The boundary where enemy wiil destroy

    [SerializeField] private float value = 100;

    public float Value => value;

    private bool isPause = false;
    void Awake() {
        //InvokeRepeating("fire", 0 ,fireInterval);
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        pool = FindObjectOfType<ObjectPool>();
    }

    void FixedUpdate()
    {
        if(destroyPos!=null)
        {
            if(transform.position.x<=destroyPos.position.x)
            {
                GameObject.Destroy(gameObject);
            }
        }
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
