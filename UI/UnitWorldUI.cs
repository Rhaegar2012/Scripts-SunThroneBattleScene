using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private UnitHealthSystem healthSystem;
    [SerializeField] private Image imageHealthBar;

    // Start is called before the first frame update
    
    void Start()
    {
        healthSystem.OnDamaged+=UnitHealthSystem_OnDamaged;
    }
    public void UnitHealthSystem_OnDamaged(object sender, EventArgs empty)
    {
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        imageHealthBar.fillAmount=healthSystem.GetHealthNormalized();
    }

}
