using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    //Singleton 
    public static UnitActionSystem Instance {get; private set;}
    //Events
    public event EventHandler OnSelectedUnitChanged;
    //Fields
    private Unit selectedUnit;
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogWarning("UnitActionSystem Singleton already exists");
            Destroy(gameObject);
            return;

        }
        Instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TryHandleUnitSelection())
        {
            return;
        }
    }

    private bool TryHandleUnitSelection()
    {
       if(UnitSelector.Instance.UnitSelectorActivate())
       {
            GridNode selectorCurrentNode=UnitSelector.Instance.GetCurrentNode();
            if(selectorCurrentNode.HasAnyUnit())
            {
                 Unit nodeUnit=selectorCurrentNode.GetUnit();
                 //Unit already selected
                 if(selectedUnit==nodeUnit)
                 {
                    return false;
                 }
                 //Unit is an enemy;
                 if(nodeUnit.IsEnemy())
                 {
                    return false;
                 }
                 SetSelectedUnit(nodeUnit);
                 return true;

            }

       }
       return false;

    }
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit=unit;
        OnSelectedUnitChanged?.Invoke(this,EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
