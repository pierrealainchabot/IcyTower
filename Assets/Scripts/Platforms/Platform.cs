using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void Update()
    {
        DestroyBeyondScreenBottom();       
    }

    private void DestroyBeyondScreenBottom()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        if (screenPosition.y < -32) // TODO : Hardcoded
        {
            // TODO: Beyond the scope of this exercise, but an Object Pool pattern shall be put to good use here.
            Destroy(gameObject);
        }
    }
}
