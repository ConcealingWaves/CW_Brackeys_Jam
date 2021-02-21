using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstrumentIndicator : MonoBehaviour
{
    [SerializeField] private string instrument;
    [SerializeField] private TextMeshProUGUI tmpro;

    public string Instrument
    {
        get => instrument;
        set => instrument = value;
    }

    public void TextColorTo(Color c)
    {
        tmpro.color = c;
    }

}
