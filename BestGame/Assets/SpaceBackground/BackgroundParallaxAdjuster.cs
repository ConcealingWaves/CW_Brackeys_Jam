using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallaxAdjuster : MonoBehaviour
{
    Transform baseTransform;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        baseTransform = transform.parent.transform;
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = (Vector2) baseTransform.position;
        offset *= 0.001f;
        material.SetVector("_Position", offset);
    }
}
