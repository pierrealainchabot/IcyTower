using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Component
{
    public static bool Destroyed { get; private set; }

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null && !Destroyed)
                {
                    Debug.LogWarning("Instance not found. Create object for singleton : " + typeof(T).Name);
                    _instance = CreateInstance();
                }
            }

            return _instance;
        }
    }

    private static T CreateInstance()
    {
        GameObject managerGameObject = new GameObject();
        managerGameObject.name = typeof(T).Name;
        return managerGameObject.AddComponent<T>();
    }
    
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroyed = true;
    }
}







