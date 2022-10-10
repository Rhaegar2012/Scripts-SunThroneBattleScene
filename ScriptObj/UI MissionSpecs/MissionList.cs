using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="MissionList",order=1)]
public class MissionList : ScriptableObject
{
    [SerializeField] private List<MissionSpecs> missionList; 
    public int TotalMissionNumber{get{return missionList.Count;}}
    public MissionSpecs GetMissionSpecs(int index)
    {
        return missionList[index];
    }

    

}
