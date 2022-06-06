using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject Path;

    [SerializeField]
    float timeBetweenSpawn;

    [SerializeField]
    float spawnRandomFactor;

    [SerializeField]
    int numOfEnemies = 10;

    [SerializeField]
    float moveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    { return enemyPrefab; }

    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach(Transform child in Path.transform)
        {
            waveWayPoints.Add(child);

        }
            
        return waveWayPoints; }

    public float GettimeBetweenSpawns()
    { return timeBetweenSpawn; }

    public float GetspawnRandomFactor()
    { return spawnRandomFactor; }

    public int GetnumOfEnemies()
    { return numOfEnemies; }

    public float getmoveSpeed()
    { return moveSpeed;}


}
