using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waves=new List<WaveConfig>();
    [SerializeField] float timeBetweenWaves=2f;
    [SerializeField] bool looping = true;

    WaveConfig currentWave;
    int startingWaveIndex = 0;
    Coroutine spawnEnemiesCoroutine;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
           yield return StartCoroutine(spawnWaves());
        }
        while (looping);
        
    }

    // Update is called once per frame
    void Update()
    {

      

    }

    IEnumerator spawnEnemies()
    {
            for (int i = 0; i < currentWave.getNumOfEnemies(); i++)
            {
            
             var enemy=Instantiate(currentWave.getEnemyPrefab(),currentWave.getPathWayPoints()[0].position,Quaternion.identity);
              enemy.GetComponent<EnemyPathing>().setWaveConfigForEnemy(currentWave);
              yield return new WaitForSeconds(currentWave.getTimeBetweenSpawns());
            }
        
    }

    IEnumerator spawnWaves()
    {
        for(int i=startingWaveIndex;i<waves.Count;i++)
        {
            currentWave = waves[i];
            yield return  StartCoroutine(spawnEnemies());
            yield return new WaitForSeconds(timeBetweenWaves);
            
        }
    }

}
