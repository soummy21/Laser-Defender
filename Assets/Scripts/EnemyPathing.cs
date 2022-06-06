using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{ 

    
    //variables
    int waypointIndex= 0;

    WaveConfig waveConfig;

    List<Transform> wayponits;

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    // Start is called before the first frame update
    void Start()
    {
       
        wayponits = waveConfig.GetWayPoints();
        transform.position = wayponits[waypointIndex].transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyPath();

    }

    private void EnemyPath()
    {
        if (waypointIndex <= wayponits.Count-1)
        {
            var targetPos = wayponits[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.getmoveSpeed()  * Time.deltaTime;
            transform.position = UnityEngine.Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
            if (targetPos == transform.position)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
