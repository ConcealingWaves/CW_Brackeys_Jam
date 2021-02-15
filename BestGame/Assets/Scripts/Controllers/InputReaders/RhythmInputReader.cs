using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RhythmInputReader : InputReader
{
    [SerializeField] private string part;
    private Beatmap map;
    private BeatmapLine line;

    public override void Enter()
    {
        map = FindObjectOfType<Beatmap>();
        if (map != null)
            line = map.GetPart(part);
        line.Tick += InvokeShoot;
    }

    public override void Exit()
    {
        line.Tick -= InvokeShoot;
    }
    public override void Tick()
    {
        
    }
}
