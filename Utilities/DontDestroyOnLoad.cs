using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        transform.SetParent(null);
        Object.DontDestroyOnLoad(gameObject);
    }
}
