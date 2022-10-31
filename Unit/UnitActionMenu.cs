using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionMenu : MonoBehaviour
{
    //Fields
    [SerializeField] private Transform actionMenu;
    [SerializeField] private Transform defaultButton;
    [SerializeField] private Transform captureButton;
    private bool isActive=false;
    private Unit selectedUnit;
    void Start()
    {
        UnitActionSystem.Instance.OnActionPositionSelected+=UnitActionSystem_OnActionPositionSelected;
    }

    public void UnitActionSystem_OnActionPositionSelected(object sender, EventArgs empty)
    {
        isActive=!isActive;
        selectedUnit=UnitActionSystem.Instance.GetSelectedUnit();
        ShowActionMenu(isActive);
        
    }
    public void ShowActionMenu(bool isActive)
    {
        actionMenu.gameObject.SetActive(isActive);
        if(selectedUnit.GetUnitType()==UnitType.Infantry)
        {
            captureButton.gameObject.SetActive(isActive);   
        }
        else
        {
            captureButton.gameObject.SetActive(!isActive);
        }

    }
    public void CancelActionMenu()
    {
        isActive=!isActive;
        actionMenu.gameObject.SetActive(isActive);
        UnitSelector.Instance.SetSelectorActive(true);
    }

    public Transform GetDefaultButton()
    {
        return defaultButton;
    }
    
}
