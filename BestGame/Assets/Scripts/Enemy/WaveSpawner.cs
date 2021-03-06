using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    #region public variable
    public GameObject[] enemyPrefabs;        //Prefab object of enemys
    //public float[] generateRate; //The probability of each enemy being generated
    public float[] spawnRate;
    public float[] beatsToStart;
    [SerializeField] private List<float> spawnTimers;

    public Beatmap referenceMap;

    //The boundary
    public Transform TopBoundary;
    public Transform BottomBoundary;
    public Transform LeftBoundary;
    public Transform RightBoundary;

    public Transform TopDestroyBoundary;
    public Transform BottomDestroyBoundary;
    public Transform LeftDestroyBoundary;
    public Transform RightDestroyBoundary;

    public Transform center;

    [HideInInspector]
    public float GenerateInterval =0.2f;   //Time interval of enemy generation

    [HideInInspector]
    public int waveLevel = 0;

    #endregion

    #region private variable
    private bool isStart = false;
    private bool isEnd = false;
    private bool isPause = false;
    private List<float> rateList;

    enum DIRECTION{
        UP,DOWN,LEFT,RIGHT
    };


    #endregion

    #region Life cycle function
    void Awake()
    {
        spawnTimers = new List<float>();
        for (int i = 0; i < spawnRate.Length; i++)
        {
            spawnTimers.Add(0);
        }
    }

    void Update()
    {
        for (int i = 0; i < spawnTimers.Count; i++)
        {
            spawnTimers[i] += Time.deltaTime;
            if (spawnTimers[i] >= spawnRate[i] && MusicUtility.BeatsToSeconds(beatsToStart[i], referenceMap.Bpm) <= referenceMap.TimeSinceStart)
            {
                generateEnemy(i);
                spawnTimers[i] = 0;
            }
        }
        
    }
    #endregion

    #region public function
    public void GameStart()
    {
        isStart = true;
        isEnd = isPause = false;
    }
    public void GameOver()
    {
        isEnd = true;
    }

    public void GamePause()
    {
        if(isStart&&!isEnd)
        {
            isPause = true;
            CancelInvoke();
            for(int i=0;i<transform.childCount;i++)
            {
                Transform enemy=transform.GetChild(i);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if(enemyScript)
                {
                    enemyScript.GamePause();
                }
            }
        }
    }
    public void GameResume()
    {
        if (isStart && !isEnd)
        {
            isPause = false;
            InvokeRepeating("generate", 0 ,GenerateInterval);
            for(int i=0;i<transform.childCount;i++)
            {
                Transform enemy=transform.GetChild(i);
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if(enemyScript)
                {
                    enemyScript.GameResume();
                }
            }
        }
    }

    public void LevelUp()
    {
        waveLevel++;
    }
    #endregion


    #region private function
    void generate() //Randomly generate an enemy
    {
        int enemyType=generateType();
        generateEnemy(enemyType);
    }

    int generateType() //Gets the type of enemy to generate
    {
        float rateSum =0;
        foreach(float rate in rateList)
        {
            rateSum += rate;
        }
        float rand = Random.Range(0, rateSum);
        rateSum = 0;
        int i;
        for(i=0;i<rateList.Capacity;i++)
        {
            rateSum += rateList[i];
            if (rateSum >= rand) break;
        }

        int type=i;
        return type;

    }
    
    [Range(0,90)]
    [SerializeField] private float rotateVariation;
    void generateEnemy(int enemyType)   //generate an enemy and initialize it
    {
        GameObject newEnemy=Instantiate(enemyPrefabs[enemyType]);
        newEnemy.transform.SetParent(transform);

        DIRECTION dirIndex = (DIRECTION)Random.Range(0, 4);
        Vector2 enemyPos =new Vector2(0,0);

        switch(dirIndex)
        {
            case DIRECTION.UP:
                enemyPos.y=BottomBoundary.position.y;
                enemyPos.x=Random.Range(LeftBoundary.position.x,RightBoundary.position.x);
                break;
            case DIRECTION.DOWN:
                enemyPos.y=TopBoundary.position.y;
                enemyPos.x=Random.Range(LeftBoundary.position.x,RightBoundary.position.x);
                break;
            case DIRECTION.LEFT:
                enemyPos.x=RightBoundary.position.x;
                enemyPos.y=Random.Range(BottomBoundary.position.y,TopBoundary.position.y);
                break;
            case DIRECTION.RIGHT:
                enemyPos.x=LeftBoundary.position.x;
                enemyPos.y=Random.Range(BottomBoundary.position.y,TopBoundary.position.y);
                break;
        }

        //Randomly initialize the position of the enemy
        newEnemy.transform.position = enemyPos;
        Vector3 diffVector = center.position - newEnemy.transform.position;
        newEnemy.transform.rotation = Quaternion.Euler(0,0,rotateVariation*Random.Range(-1.0f,1.0f)-Mathf.Rad2Deg*Mathf.Atan2(diffVector.x, diffVector.y));



        //initialize variable of enemy script
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        enemyScript.TopDestroyBoundary = TopDestroyBoundary.position;
        enemyScript.BottomDestroyBoundary = BottomDestroyBoundary.position;
        enemyScript.LeftDestroyBoundary = LeftDestroyBoundary.position;
        enemyScript.RightDestroyBoundary = RightDestroyBoundary.position;
        enemyScript.bounded = true;

    }
    #endregion
}
