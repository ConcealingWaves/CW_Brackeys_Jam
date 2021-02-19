using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingShooters : EntityController
{
    [SerializeField] private List<EntityController> shootersToAlternate;
    private int currentAlternation;

    public int Count => shootersToAlternate.Count;

    public List<EntityController> ShootersToAlternate
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
        InvokeShootAction();
    }

    public override void InvokeShootAction()
    {
        shootersToAlternate[currentAlternation % shootersToAlternate.Count].InvokeShootAction();
        currentAlternation++;
    }

    public void AddShooter(EntityController s)
    {
        shootersToAlternate.Add(s);
    }

    public void RemoveShooter(EntityController s)
    {
        shootersToAlternate.Remove(s);
    }
}
