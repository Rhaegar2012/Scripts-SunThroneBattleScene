using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionMenu : MonoBehaviour
{
    //Fields
    [SerializeField] private GameObject actionMenu;
    private bool isActive=false;
    void Start()
    {
        UnitActionSystem.Instance.OnActionPositionSelected+=UnitSelector_OnActionPositionSelected;

    }

    public void UnitSelector_OnActionPositionSelected(object sender, EventArgs empty)
    {
        isActive=!isActive;
        actionMenu.SetActive(isActive);
    }
}
