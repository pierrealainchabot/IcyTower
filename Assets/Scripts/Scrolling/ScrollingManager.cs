using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScrollingManager : MonoBehaviour
{
    public static ScrollingManager Instance { get; private set; }

    public float autoScrollingSpeed = 1;
    public bool automaticStart = false;
    
    private List<IScrollable> _scrollables = new List<IScrollable>();
    private bool _started = false;

    void Awake()
    {
        SingletonPattern();
        _started = automaticStart;
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
