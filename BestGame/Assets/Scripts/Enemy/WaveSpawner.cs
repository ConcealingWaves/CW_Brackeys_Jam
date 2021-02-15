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
    private List<float> rateList;

    #endregion

    #region Life cycle function
    void Awake()
    {
        rateList = new List<float>(generateRate);
        GameStart();
    }

    void Update()
    {
        
    }
    #endregion

    #region public function
    public void GameStart()
    {
        InvokeRepeating("generate", 0 ,GenerateInterval);
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

        //initialize variable of enemy script
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        enemyScript.destroyPos = LeftBoundary;

    }
    #endregion
}
