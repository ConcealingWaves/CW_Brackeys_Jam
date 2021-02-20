using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ExternalShooterController : EntityController
{
    [SerializeField] private ExternalShooter[] externalShooters;


    private void Start()
    {

    }

    public override void ShootAction()
    {
        if (!AllowedToShoot) return;
        Shoot();
    }

    private void Shoot()
    {
        foreach (var e in externalShooters)
        {
            if (e == null) continue;
            e.Shoot(this);
        }
    }

    public override void InvokeShootAction()
    {
        Shoot();
    }
}