using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Transform> levelWaypoints;
    [SerializeField] GameObject newEnemy;
    [SerializeField] float timeBetweenEnemies;
    [SerializeField] float timeBetweenWaves;
    float wavesTimer;
    int enemiesOnMap = 0;
    int enemyCounter = 0;
    int wavesCounter = 0;
    int wavesToWin = 4;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        wavesTimer = timeBetweenWaves;
    }
    // Update is called once per frame
    void Update()
    {
        if(enemiesOnMap == 0)
        {
            if (wavesCounter > wavesToWin)
            {
                GameManagement.instance.Victory();
            }

            Debug.Log("Wave Ended");
            wavesTimer -= Time.deltaTime;
            if (wavesTimer < 0)
            {
                wavesCounter++;
                wavesTimer = timeBetweenWaves;
                enemyCounter = 0;
                enemiesOnMap = enemyPrefabs.Count;
                InvokeRepeating(nameof(SendWaves), 0, timeBetweenEnemies);
            }                
        }
    }

    public void SendWaves()
    {
        Instantiate(enemyPrefabs[enemyCounter], levelWaypoints[0].position, enemyPrefabs[0].transform.rotation);
        //enemyPrefabs.Remove(enemyPrefabs[0]); //Funcionamento 
        
        enemyCounter++;

        if (enemyCounter >= enemyPrefabs.Count)
        {
            enemyPrefabs.Add(newEnemy);
            CancelInvoke();
        }
    }

    public List<Transform> GetWaypoints()
    {
        return levelWaypoints;
    }

    public void RemoveEnemyFromMap()
    {
        enemiesOnMap--;
    }
}
