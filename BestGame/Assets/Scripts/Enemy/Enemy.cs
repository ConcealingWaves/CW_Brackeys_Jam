using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed=5.0f;    //The move speed of the enemy
    [HideInInspector]
    public Transform destroyPos;    //The boundary where enemy wiil destroy
    void Update()
    {
        Vector3 pos=transform.position;
        pos.x-=moveSpeed*Time.deltaTime;
        transform.position=pos;
        if(destroyPos!=null)
        {
            if(pos.x<=destroyPos.position.x)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
