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
    public event EventHandler OnDeselectedUnit;
    //Fields
    private Unit selectedUnit;
    private BaseAction baseAction;
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
        TryHandleSelectedAction();

    }

    private bool TryHandleUnitSelection()
    {
       if(UnitSelector.Instance.UnitSelectorActivate())
       {
            GridNode selectorCurrentNode=UnitSelector.Instance.GetCurrentNode();
           
            if(selectorCurrentNode.HasAnyUnit())
            {
                Unit nodeUnit=selectorCurrentNode.GetUnit();
                Debug.Log($"Unit in node {nodeUnit.GetUnitType()}");
                
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
            Vector2 unitSelectorActionNodePosition= UnitSelector.Instance.GetGridPosition();
            if(selectedUnit!=null && !baseAction.IsValidGridPositionList(unitSelectorActionNodePosition))
            {
                DeselectUnit();
            }
            

       }
       return false;

    }

    private void TryHandleSelectedAction()
    {
        Vector2 unitSelectorActionNodePosition = UnitSelector.Instance.GetGridPosition();
        if(UnitSelector.Instance.UnitSelectorActivate() && selectedUnit!=null)
        {
            if(baseAction.IsValidGridPositionList(unitSelectorActionNodePosition))
            {
                //TODO Deploy Action Menu and wrap take action on menu confirmation
                baseAction.TakeAction(unitSelectorActionNodePosition,ClearBusy);
                SetSelectedUnit(null);
            }
        }
    }
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit=unit;
        if(selectedUnit!=null)
        {
            SetAction();
        }
        OnSelectedUnitChanged?.Invoke(this,EventArgs.Empty);
    }
    private void SetAction()
    {
        baseAction=selectedUnit.GetAction();
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
    public void ClearBusy()
    {

    }
    private void DeselectUnit()
    {
        selectedUnit=null;
        baseAction=null;
        OnDeselectedUnit?.Invoke(this,EventArgs.Empty);
    }
}
