using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private const float FIXED_Z_COORDINATE = -10.0f;

    [SerializeField] private float followBehindFactor;
    [SerializeField] private float smoothTime;
    [SerializeField] private EntityController toFollow;

    private Vector2 targetPosition;
    private Vector2 temp_velocity;
    

    private void FixedUpdate()
    {
        if(toFollow!=null)
            targetPosition = (Vector2)toFollow.transform.position + toFollow.MoveVector*followBehindFactor;
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        Vector2 toVector = Vector2.SmoothDamp(transform.position, targetPosition, ref temp_velocity, smoothTime);
        transform.position = new Vector3(toVector.x, toVector.y, FIXED_Z_COORDINATE);
    }
}
