using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Scrollable
{
    public override void ApplyDownwardScrolling(float amount)
    {
        transform.position += Vector3.down * amount;
    }
}
