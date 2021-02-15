using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInputReader : InputReader
{
    public override void Tick()
    {
        RotationInput = Input.GetAxisRaw("Horizontal");
        ThrustEngaged = Input.GetKey(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.Space))
            InvokeShoot();
    }
}
