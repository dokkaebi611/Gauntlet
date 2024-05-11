using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : PlayerController
{
    protected override void Start()
    {
        base.Start();
        // Warrior-specific initialization
        speed = 5f; // Adjust as needed
        // Additional warrior-specific properties
    }

    // Add warrior-specific methods and properties if needed
}