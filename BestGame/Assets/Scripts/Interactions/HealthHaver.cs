using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHaver : MonoBehaviour, IDamageable
{
    public delegate void HitAction(float d, HealthHaver h);

    public static event HitAction OnHit;
    public event HitAction OnThisHit;

    public delegate void DieAction(HealthHaver d);

    public static event DieAction OnDie;
    public event DieAction OnThisDie;

    [SerializeField] private float baseHealth;
    [SerializeField] private float health;

    private bool isDead;

    public float Health
    {
        get => health;
        set => health = Mathf.Max(value, 0);
    }

    private void Start()
    {
        isDead = false;
        health = baseHealth;
    }
    
    private void Die()
    {
        RaiseDeathEvents();
        transform.parent = null;
        StartCoroutine(KillIn(0.1f));
    }
    
    public void TakeHit(float dmg)
    {
        Health -= dmg;
        RaiseHitEvents(dmg);
        if (Health <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }

    private void RaiseHitEvents(float dmg)
    {
        OnHit?.Invoke(dmg, this);
        OnThisHit?.Invoke(dmg, this);
    }

    private void RaiseDeathEvents()
    {
        OnDie?.Invoke(this);
        OnThisDie?.Invoke(this);
    }
    
    IEnumerator KillIn(float s)
    {
        yield return new WaitForSeconds(s);
        Destroy(gameObject);
    }
}
