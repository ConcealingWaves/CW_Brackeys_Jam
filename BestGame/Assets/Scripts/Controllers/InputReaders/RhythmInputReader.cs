using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu]
public class RhythmInputReader : InputReader
{
    [SerializeField] private string part;

    private BeatmapLine.TickAction currentDelegate;

    public string Part
    {
        get => part;
    }

    public override void Enter(EntityController cont)
    {
        currentDelegate = s => CheckInvokeShoot(s,cont);
        BeatmapLine.Tick += currentDelegate;
    }

    public override void Exit(EntityController cont)
    {
        BeatmapLine.Tick -= currentDelegate;
    }
    public override void Tick(EntityController cont)
    {
        
    }

    private void CheckInvokeShoot(string tickedLine, EntityController cont)
    {
        if(part == tickedLine)
            InvokeShoot(cont);
    }
}
