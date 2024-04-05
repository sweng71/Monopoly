using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Board_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Test that correct positions for each tile is reported

        var p0 = GetPosition(0);
        Debug.Log("Position 0: " + p0.position.ToString());

        var p10 = GetPosition(10);
        Debug.Log("Position 10;" + p10.position.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetPosition(int index)
    {
        string name = "Pos_" + index;
        var child = transform.Find(name);

        if (child != null)
        {
            return child;
        }
        return null;
    }
    public Transform GetJailPosition()
    {
        return GetPosition(99);
    }
    public Transform GetFreeParkingPosition()
    {
        return GetPosition(20);
    }
}
