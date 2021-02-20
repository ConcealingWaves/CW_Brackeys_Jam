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
public class PursuePlayer : InputReader
{
    private const float NEGLIGIBLE_DIFFERENCE = 0.33f;
    
    private Transform toPursue;

    public override void Enter(EntityController cont)
    {
        List<Absorber> candidates = FindObjectsOfType<Absorber>().Where(s => s.gameObject.layer == LayerMask.NameToLayer("Friendly")).ToList();
//        if (candidates.Count == 0) Debug.LogError("Can't find an appropriate player to pursue!");
        if (candidates.Count > 1) Debug.LogWarning("Found multiple player candidates to pursue! I picked the first one.");
        if(candidates.Count > 0)
            toPursue = candidates[0].transform;
    }

    public override void Tick(EntityController cont)
    {
        if (toPursue == null) return;
        Transform contTransform = cont.transform;
        Vector3 differenceVector = (toPursue.position - contTransform.position).normalized;
        Vector3 cross = Vector3.Cross(differenceVector, contTransform.up);
        if (cross.z > NEGLIGIBLE_DIFFERENCE)
        {
            cont.RotationalInput = 1;
        }
        else if (cross.z < NEGLIGIBLE_DIFFERENCE)
        {
            cont.RotationalInput = -1;
        }
        else
        {
            cont.RotationalInput = 0;
        }
    }
}

