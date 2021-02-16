using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorberExtension : MonoBehaviour
{
    private Absorber primaryAbsorber;

    public void SetPrimaryAbsorber(Absorber a)
    {
        primaryAbsorber = a;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Absorbable absorbable = other.gameObject.GetComponent<Absorbable>();
        if (primaryAbsorber != null && absorbable != null)
        {
            primaryAbsorber.AddToAbsorbedUnits(absorbable);
        }
    }
}
