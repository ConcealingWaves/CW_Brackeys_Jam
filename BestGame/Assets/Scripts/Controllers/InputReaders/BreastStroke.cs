using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu]
public class BreastStroke : InputReader
{
    [SerializeField] private float onTime;
    [SerializeField] private float offTime;



    public override void Tick(EntityController cont)
    {
        cont.MovementInput = cont.InternalTimer % (onTime + offTime) < onTime;
    }
}