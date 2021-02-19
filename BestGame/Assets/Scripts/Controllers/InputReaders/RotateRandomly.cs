using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RotateRandomly : InputReader
{
    public float rotateRestMin;
    public float rotateRestMax;
    [Range(0, 1)] public float rotateFrequencyMin;
    [Range(0, 1)] public float rotateFrequencyMax;
    public override void Enter(EntityController cont)
    {
        cont.rotateRestTime = Random.Range(rotateRestMin,rotateRestMax);
    }

    public override void Tick(EntityController cont)
    {
        if (cont.timeSinceLastRotateStart >= cont.rotateRestTime)
        {
            float timeToRotate = Random.Range(rotateRestMin, rotateRestMax);
            cont.rotateRestTime = timeToRotate;
            cont.RotateForSeconds(Random.Range(0,2)*2-1, Random.Range(rotateFrequencyMin,rotateFrequencyMax)*timeToRotate);
        }
    }
}