using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemDontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        EventSystem[] eventSystemList=FindObjectsOfType<EventSystem>();
        if(eventSystemList.Length>1)
        {
            for(int i=1;i<eventSystemList.Length;i++)
            {
                Destroy(eventSystemList[i].gameObject);
            }
            return;
        }
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
    }
}
