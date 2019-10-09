using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : Singleton<ScrollingManager>
{
    public float autoScrollingSpeed = 1;
    public bool automaticStart = false;
    
    private List<IScrollable> _scrollables = new List<IScrollable>();
    private bool _started = false;
    
    protected override void Awake()
    {
        base.Awake();
        _started = automaticStart;
    }

    public void StartScrolling()
    {
        _started = true;
    }

    void Update()
    {
        if (_started)
        {
            DownwardAutoScrolling();    
        }
    }

    public void RegisterScrollable(IScrollable scrollable)
    {
        _scrollables.Add(scrollable);
    }

    public void DeregisterScrollable(IScrollable scrollable)
    {
        _scrollables.Remove(scrollable);
    }

    private void DownwardAutoScrolling()
    {
        float scrollAmountY = CalculateDownwardAutoScrollAmount();
        _scrollables.ForEach(scrollable => scrollable.ApplyDownwardScrolling(scrollAmountY));
    }

    private float CalculateDownwardAutoScrollAmount()
    {
        return autoScrollingSpeed * Time.deltaTime;
    }
}
