using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalShooter : MonoBehaviour
{
    private ObjectPool pool;
    [SerializeField] private Projectile projectile;


    private void Start()
    {
        pool = FindObjectOfType<ObjectPool>();
        if (pool == null)
            Debug.LogError("No object pool found! Please create one.");
    }
    
    public void Shoot(ExternalShooterController from)
    {
        Projectile toSpawn = pool.Spawn(projectile.gameObject, projectile.name, transform.position, transform.rotation).GetComponent<Projectile>();
        if(from.col!=null)
            Physics2D.IgnoreCollision(toSpawn.col, from.col);
        toSpawn.gameObject.layer = from.gameObject.layer;
        toSpawn.MyPool = pool;
        toSpawn.MyPoolTag = projectile.name;
        toSpawn.Speed += from.MoveVector.magnitude;
    }
}
