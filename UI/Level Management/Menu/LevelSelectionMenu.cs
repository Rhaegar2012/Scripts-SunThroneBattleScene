using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class LevelSelectionMenu:Menu<LevelSelectionMenu>
{
    //Events
    public  event EventHandler OnPlayLevelCalled;
    //Fields
    private MissionSelector missionSelector;
    private MissionSpecs currentMission;
    [SerializeField] private MissionList missionList;
    [SerializeField] private TextMeshProUGUI missionTitleText;
    [SerializeField] private TextMeshProUGUI missionDescriptionText;
    [SerializeField] private Image missionImageThumbnail;
    [SerializeField] private LevelSelectionMenu levelSelectionMenu;
    [SerializeField] private GameObject selectedButton;
    protected override void Awake()
    {
        base.Awake();
        missionSelector= new MissionSelector(missionList);
        currentMission=missionSelector.GetCurrentMissionSpecs();
        UpdateInfo();
    }
    public void OnNextLevelPressed()
    {
        missionSelector.IncreaseMissionIndex();
        currentMission=missionSelector.GetCurrentMissionSpecs();
        UpdateInfo();
    }
    public void OnPreviousLevelPressed()
    {
        missionSelector.DecreaseMissionIndex();
        currentMission=missionSelector.GetCurrentMissionSpecs();
        UpdateInfo();

    }
    public void OnPlayLevelPressed()
    {

        LevelLoader.LoadLevel(currentMission.MissionId);
        StartCoroutine(HoldForLevelLoad());
        MenuManager.Instance.CloseAllMenus();
        OnPlayLevelCalled?.Invoke(this,EventArgs.Empty);
        
    }
    private void UpdateInfo()
    {

        missionTitleText.text=currentMission.MissionTitle;
        missionDescriptionText.text=currentMission.MissionDescription;
        missionImageThumbnail.sprite=currentMission.MissionThumbnail;
    }
    public void SetFirstSelectedButton()
    {
        base.firstSelectedButton=selectedButton;
    }
    private IEnumerator HoldForLevelLoad()
    {
        yield return null;
    }

}
