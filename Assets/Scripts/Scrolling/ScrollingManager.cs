using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScrollingManager : MonoBehaviour
{
    public static ScrollingManager Instance { get; private set; }

    public int autoScrollingSpeed = 1;
    
    private List<IScrollable> _scrollables = new List<IScrollable>();

    void Awake()
    {
        SingletonPattern();
    }

    private void SingletonPattern()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        DownwardAutoScrolling();
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
