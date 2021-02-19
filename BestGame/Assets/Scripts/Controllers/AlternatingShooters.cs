using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingShooters : EntityController
{
    [SerializeField] private List<ShooterController> shootersToAlternate;
    private int currentAlternation;

    public int Count => shootersToAlternate.Count;

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
        if (!AllowedToShoot || shootersToAlternate.Count == 0){ return;}
        shootersToAlternate[currentAlternation % shootersToAlternate.Count].InvokeShootAction();
        currentAlternation++;
    }

    public void AddShooter(ShooterController s)
    {
        shootersToAlternate.Add(s);
    }

    public void RemoveShooter(ShooterController s)
    {
        shootersToAlternate.Remove(s);
    }
}
