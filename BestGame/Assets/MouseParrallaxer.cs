using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParrallaxer : MonoBehaviour
{
    Vector2 initialMousePos;
    Vector2 targetOffset;
    Vector2 currentOffset;
    [SerializeField] Material spaceMat;
    void Start()
    {
        initialMousePos = GetMousePos();
        currentOffset = Vector2.zero;
        targetOffset = currentOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = GetMousePos() - initialMousePos;
        targetOffset = offset;
        LerpTowardsTargetOffset();
    }

    void LerpTowardsTargetOffset()
    {
        currentOffset = Vector2.Lerp(currentOffset, targetOffset, 0.01f);
        spaceMat.SetVector("_Position", currentOffset * 0.000001f);
    }

    private Vector2 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        return (Vector2) mousePos;
    }
}
