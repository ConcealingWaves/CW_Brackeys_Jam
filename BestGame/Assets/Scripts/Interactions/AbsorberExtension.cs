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
}
