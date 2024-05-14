using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : PlayerController
{
    protected override void Start()
    {
        base.Start();
        speed *= 2f;
    }
}
