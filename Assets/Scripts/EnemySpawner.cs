using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<WaveConfig> waveConfigs;
    [SerializeField]
    int startingWave = 0;

    [SerializeField]
    bool enemyLoop = false;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (enemyLoop);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i=startingWave;i<=waveConfigs.Count-1;i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnEnemyWave(currentWave));
        }
    }
    private IEnumerator SpawnEnemyWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount <= waveConfig.GetnumOfEnemies(); enemyCount++)
        {
            var newEnemy =Instantiate<GameObject>(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig); 
            yield return new WaitForSeconds(waveConfig.GettimeBetweenSpawns());
        }
    }


}
