using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Absorber))]
public class TentaclesRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer linePrefab;
    private Absorber absorber;

    private List<LineRenderer> linesOnScreen;
    private List<LineRenderer> oldLinesOnScreen;

    private void Awake()
    {
        absorber = GetComponent<Absorber>();
        linesOnScreen = new List<LineRenderer>();
        oldLinesOnScreen = new List<LineRenderer>();
    }

    private void Update()
    {
        DrawLines();
    }

    private void DrawLines()
    {
        foreach(var v in oldLinesOnScreen) Destroy(v.gameObject);
        oldLinesOnScreen.RemoveAll(s=>true);
        oldLinesOnScreen.AddRange(linesOnScreen);
        linesOnScreen.RemoveAll(s=>true);
        List<Transform> absorbed = GetComponentsInChildren<Absorbable>().ToList().Select(s => s.transform).ToList();
        foreach (var a in absorbed)
        {
            LineRenderer thisLine = Instantiate(linePrefab, Vector3.zero, quaternion.identity);
            thisLine.SetPosition(0,a.position);
            thisLine.SetPosition(1,transform.position);
            linesOnScreen.Add(thisLine);
        }
    }
}
