    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInputReader : InputReader
{
    public override void Tick(EntityController cont)
    {
        cont.RotationalInput = Input.GetAxisRaw("Horizontal");
        cont.MovementInput = Input.GetKey(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.Space))
            InvokeShoot(cont);
    }
}
