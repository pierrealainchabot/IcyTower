using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scrollable : MonoBehaviour, IScrollable
{ 
    public abstract void ApplyDownwardScrolling(float amount);

    void Start()
    {
        ScrollingManager.Instance.RegisterScrollable(this);
    }

    private void OnDestroy()
    {
        ScrollingManager.Instance.DeregisterScrollable(this);
    }
}
