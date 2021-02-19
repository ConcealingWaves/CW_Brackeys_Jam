using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KeepMoving : InputReader
{
    public override void Tick(EntityController cont)
    {
        cont.MovementInput = true;
        cont.RotationalInput += Random.Range(-1.0f,1.0f);
    }
}
