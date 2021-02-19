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
        float realOnTime = MusicUtility.BeatsToSeconds(onTime,115); //i dont care (the solution is to have cont reference the beatmap and read that but we only have 1 song for this game jam so whatever)
        float realOffTime = MusicUtility.BeatsToSeconds(offTime, 115);
        cont.MovementInput = cont.InternalTimer % (realOnTime + realOffTime) < realOnTime;
    }
}