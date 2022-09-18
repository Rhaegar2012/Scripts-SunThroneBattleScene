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
    }
    public void Damage()
    {
        Destroy(gameObject);
        OnAnyUnitDestroyed?.Invoke(this,unit);
    }


}
