using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShooterController : EntityController
{
    [SerializeField] private Projectile projectile;
    private ObjectPool pool;


    private void Start()
    {
        pool = FindObjectOfType<ObjectPool>();
        if (pool == null)
            Debug.LogError("No object pool found! Please create one.");
    }

    public override void ShootAction()
    {
        if (!AllowedToShoot) return;
        Shoot();
    }

    private void Shoot()
    {
        Projectile toSpawn = pool.Spawn(projectile.gameObject, projectile.name, transform.position, transform.rotation).GetComponent<Projectile>();
        if(col!=null)
            Physics2D.IgnoreCollision(toSpawn.col, col);
        toSpawn.gameObject.layer = gameObject.layer;
        toSpawn.MyPool = pool;
        toSpawn.MyPoolTag = projectile.name;
        toSpawn.Speed += MoveVector.magnitude;
    }

    public void InvokeShootAction()
    {
        Shoot();
    }
}
