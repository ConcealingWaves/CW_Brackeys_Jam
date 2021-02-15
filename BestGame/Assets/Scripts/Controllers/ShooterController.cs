using System;
using System.Collections;
using System.Collections.Generic;
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

    protected override void ShootAction()
    {
        Projectile toSpawn = pool.Spawn(projectile.gameObject, projectile.name, transform.position, transform.rotation).GetComponent<Projectile>();
        Physics2D.IgnoreCollision(col, toSpawn.col);
        toSpawn.MyPool = pool;
        toSpawn.MyPoolTag = projectile.name;
    }
}
