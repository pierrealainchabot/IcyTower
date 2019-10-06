using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformManager : MonoBehaviour
{

    public int PlatformsCount { get; private set; }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void SpawnPlatform()
    {
        SpawnPlatformOrFloor();
        PlatformsCount++;
    }

    public void SpawnPlatformOrFloor()
    {
        if (IsFullFloorNeeded())
        {
            SpawnFullFloor();
        }
        else
        {
            SpawnSmallPlatform();
        }
    }

    public bool IsFullFloorNeeded()
    {
        return PlatformsCount > 0 && (PlatformsCount % 10 == 0);
    }

    public void SpawnFullFloor()
    {
        
    }

    public void SpawnSmallPlatform()
    {
        
    }
}
