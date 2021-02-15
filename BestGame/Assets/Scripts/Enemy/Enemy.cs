using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float moveSpeed=5.0f;    //The move speed of the enemy
    public float fireInterval = 0.2f;
    public float bulletSpeed = 10.0f;
    [HideInInspector]
    public Transform destroyPos;    //The boundary where enemy wiil destroy

    private bool isPause = false;
    void Awake() {
        InvokeRepeating("fire", 0 ,fireInterval);
    }
    void Update()
    {
        if(!isPause)
            move();
    }
    public void GamePause()
    {
        isPause = true;
        CancelInvoke();

    }
    public void GameResume()
    {
        isPause =false;
        InvokeRepeating("fire", 0 ,fireInterval);
    }

    void move()
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

    void fire()
    {
        GameObject newBullet=Instantiate(bulletPrefab);
        newBullet.transform.SetParent(transform);
        newBullet.transform.position = transform.position;
        EnemyBullet bulletScript = newBullet.GetComponent<EnemyBullet>();
        bulletScript.destroyPos =destroyPos;
        bulletScript.moveSpeed = bulletSpeed;
    }
}
