using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KeepMoving : InputReader
{
    public override void Tick()
    {
        ThrustEngaged = true;
    }
}
