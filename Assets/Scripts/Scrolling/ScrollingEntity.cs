using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingEntity : Scrollable
{
    public float ScrollingSpeedMultiplier = 1;
    
    public override void ApplyDownwardScrolling(float amount)
    {
        transform.position += Vector3.down * (amount * ScrollingSpeedMultiplier);
    }
}
