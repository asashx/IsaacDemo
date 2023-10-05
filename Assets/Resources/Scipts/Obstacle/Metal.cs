using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : Obstacle
{
    protected override void Start()
    {
        isDestructible = false;
    }
}
