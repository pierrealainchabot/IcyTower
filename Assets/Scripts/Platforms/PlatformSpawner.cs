using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public Platform platformPrefab;

    public Platform Spawn()
    {
        var newPlatform = Instantiate(platformPrefab);
        newPlatform.transform.position = transform.position;

        return newPlatform;
    }
}
