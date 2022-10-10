using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelector
{
    private MissionList missionList;
    private MissionSpecs currentMissionSpecs;
    private int missionListIndex=0;


    public MissionSelector(MissionList missionList)
    {
        this.missionList=missionList;
    }
    public MissionSpecs GetCurrentMissionSpecs()
    {
        return this.missionList.GetMissionSpecs(missionListIndex);
    }
    public void IncreaseMissionIndex()
    {
        missionListIndex++;
    }
    public void DecreaseMissionIndex()
    {
        missionListIndex--;
    }


}
