using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHaver : MonoBehaviour, IDamageable
{
    public delegate void HitAction(float d, HealthHaver h);

    public static event HitAction OnHit;

    public delegate void DieAction(HealthHaver d);

    public static event DieAction OnDie;

    [SerializeField] private float baseHealth;
    [SerializeField] private float health;

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    private void Start()
    {
        health = baseHealth;
    }
    
    private void Die()
    {
        Destroy(gameObject); //replace with some death sequence
        OnDie?.Invoke(this);
    }
    
    public void TakeHit(float dmg)
    {
        Health -= dmg;
        OnHit?.Invoke(dmg, this);
        if (Health <= 0)
        {
            Die();
        }
    }
}
