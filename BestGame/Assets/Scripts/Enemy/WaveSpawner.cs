using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    #region public variable
    public GameObject[] enemyPrefabs;        //Prefab object of enemys
    public float[] generateRate;            //The probability of each enemy being generated

    //The boundary
    public Transform TopBoundary;
    public Transform BottomBoundary;
    public Transform LeftBoundary;

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

    #endregion

    #region Life cycle function
    void Awake()
    {
        rateList = new List<float>(generateRate);
    }

    void Update()
    {
        
    }
    #endregion

    #region public function
    public void GameStart()
    {

        isStart = true;
        isEnd = isPause = false;
        InvokeRepeating("generate", 0 ,GenerateInterval);
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

    void generateEnemy(int enemyType)   //generate an enemy and initialize it
    {
        GameObject newEnemy=Instantiate(enemyPrefabs[enemyType]);
        newEnemy.transform.SetParent(transform);

        //Randomly initialize the position of the enemy
        float posY=Random.Range(BottomBoundary.position.y,TopBoundary.position.y);
        Vector3 pos = transform.position;
        pos.y = posY;
        newEnemy.transform.position = pos;
        newEnemy.transform.rotation = Quaternion.Euler(0,0,90);

        //initialize variable of enemy script
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        enemyScript.destroyPos = LeftBoundary;

    }
    #endregion
}
