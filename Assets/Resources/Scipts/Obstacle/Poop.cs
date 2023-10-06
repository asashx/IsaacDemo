using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : Obstacle
{
    protected override void Start()
    {
        base.Start();
        isAttackable = true;
    }

}
