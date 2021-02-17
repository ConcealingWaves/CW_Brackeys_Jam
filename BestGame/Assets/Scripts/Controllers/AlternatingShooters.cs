using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingShooters : EntityController
{
    [SerializeField] private List<ShooterController> shootersToAlternate;
    private int currentAlternation;

    public List<ShooterController> ShootersToAlternate
    {
        get => shootersToAlternate;
        set => shootersToAlternate = value;
    }

    protected override void Awake()
    {
        base.Awake();
        currentAlternation = 0;
    }

    public override void ShootAction()
    {
        if (!AllowedToShoot) return;
        print(currentAlternation);
        shootersToAlternate[currentAlternation % shootersToAlternate.Count].InvokeShootAction();
        currentAlternation++;
    }
}
