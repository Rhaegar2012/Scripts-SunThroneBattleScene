using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MissionSpecs
{
   [SerializeField] protected int missionID;
   [SerializeField] protected string missionTitle;
   [SerializeField] protected Sprite missionThumbnail;
   [SerializeField] [Multiline] protected string missionDescription;

   public int MissionId {get {return missionID;} set{missionID=value;}}
   public string MissionTitle {get{return missionTitle;} set{missionTitle=value;}}
   public string MissionDescription {get{return missionDescription;} set{missionTitle=value;}}
   public Sprite MissionThumbnail{get{return missionThumbnail;}set{missionThumbnail=value;}} 
}
