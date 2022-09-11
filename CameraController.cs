using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    //Singleton
    public static CameraController Instance {get; private set;}
    //Fields
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GameObject playerUnitSelector;
    void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogWarning("CameraController singleton instance already exists");
            Destroy(gameObject);
            return;
        }
        Instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateFollowingUnit(Transform unitTransform)
    {
        cinemachineVirtualCamera.Follow=unitTransform;

    }
}
