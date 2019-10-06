using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
    public Camera mainCamera;
    public float floorHeightInWorldUnit = 5;
    public int pixelPerUnit = 16;
    public PlatformSpawner platformSpawner;
    
    private float _elapsedDistance;

    private void Update()
    {
        SpawnMissingPlatforms();
    }

    private void SpawnMissingPlatforms()
    {
        while (GetPlatformSpawnerScreenPositionY() < PlatformSpawnerSafeZone())
        {
            platformSpawner.Spawn();
            MovePlatformSpawnerToNextFloor();
        }
    }

    private float GetPlatformSpawnerScreenPositionY()
    {
        return mainCamera.WorldToScreenPoint(platformSpawner.transform.position).y;
    }
    
    private float PlatformSpawnerSafeZone()
    {
        return Screen.height + ((floorHeightInWorldUnit + pixelPerUnit) * 2);
    }

    private void MovePlatformSpawnerToNextFloor()
    {
        platformSpawner.transform.position += (Vector3.up * floorHeightInWorldUnit);
    }
}
