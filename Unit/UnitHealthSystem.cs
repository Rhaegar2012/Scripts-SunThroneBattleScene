using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthSystem : MonoBehaviour
{
    //Events
    public static event EventHandler<Unit> OnAnyUnitDestroyed;
    public event EventHandler OnDamaged;
    //Fields
    private int healthPoints;
    private int totalHealth;
    private Unit unit;
    void Awake()
    {
        unit= GetComponent<Unit>();
        healthPoints=10;
        totalHealth=healthPoints;
    }
    public void Damage(int damageAmount)
    {
        healthPoints-=damageAmount;
        OnDamaged?.Invoke(this,EventArgs.Empty);
        if(healthPoints<=0)
        {
            Die();
        }
       
    }
    public int GetHealth()
    {
        return healthPoints;
    }
    private void Die()
    {
        Destroy(gameObject);
        OnAnyUnitDestroyed?.Invoke(this,unit);
    }
    public float GetHealthNormalized()
    {
        return (float)healthPoints/totalHealth;
    }



}
