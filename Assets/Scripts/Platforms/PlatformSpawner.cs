using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    public int spawningAreaWidth;
    public Platform platformPrefab;

    private float _leftLimit;
    private float _rightLimit;

    private void Awake()
    {
        var halfSpawningAreaWidth = HalfSpawingAreaWidth();
        _leftLimit = -halfSpawningAreaWidth;
        _rightLimit = halfSpawningAreaWidth;
    }

    private void OnDrawGizmos()
    {
        var halfSpawningAreaWidth = HalfSpawingAreaWidth();
        
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawLine(new Vector3(halfSpawningAreaWidth, 1000, 0), new Vector3(halfSpawningAreaWidth, -1000, 0));
        UnityEditor.Handles.DrawLine(new Vector3(-halfSpawningAreaWidth, 1000, 0), new Vector3(-halfSpawningAreaWidth, -1000, 0));
    }

    private float HalfSpawingAreaWidth()
    {
        return spawningAreaWidth / 2;
    }
    
    public Platform Spawn()
    {
        var newPlatform = Instantiate(platformPrefab);
        newPlatform.transform.position = new Vector3(GetRandomPositionX(), transform.position.y, 0);

        return newPlatform;
    }
    
    private float GetRandomPositionX()
    {
        var halfPlatformWidth = 10;// TODO : Hardcoded
        var realLeftLimit = _leftLimit + halfPlatformWidth;
        var realRightLimit = _rightLimit - halfPlatformWidth;
        
        return Random.Range(realLeftLimit, realRightLimit);
    }
}
