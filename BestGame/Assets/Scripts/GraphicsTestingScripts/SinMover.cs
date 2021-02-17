using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMover : MonoBehaviour
{
    Vector2 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
        float x_mod = Mathf.Sin(time);
        float y_mod = Mathf.Cos(time);
        Vector2 pos_vector = new Vector2(x_mod, y_mod);
        transform.position = initialPosition + pos_vector * 3f;
        Vector2 perpendicularVector = Vector2.Perpendicular(pos_vector);
        float targetAngle = Vector2.SignedAngle(Vector2.up, perpendicularVector) + 180f;
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
    }
}
