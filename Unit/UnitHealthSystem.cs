using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthSystem : MonoBehaviour
{
    //Events
    public static event EventHandler<Unit> OnAnyUnitDestroyed;
    //Fields
    private int healthPoints;
    private Unit unit;
    void Awake()
    {
        unit= GetComponent<Unit>();
        healthPoints=10;
    }
    public void Damage(int damageAmount)
    {
        healthPoints-=damageAmount;
        Debug.Log($"Remaining health: {healthPoints}");
        if(healthPoints<=0)
        {
            Die();
        }
       
    }
    private void Die()
    {
        Destroy(gameObject);
        OnAnyUnitDestroyed?.Invoke(this,unit);
    }


}
