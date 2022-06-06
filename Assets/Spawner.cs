using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject Meteor;

    [SerializeField]
    float timeBetSpawn = 2f;

    [SerializeField]
    float minX = -2f;

    [SerializeField]
    float maxX = 2f;

    [SerializeField]
    float speed = 10f;

   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMeteors());
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            var newMeteor = Instantiate<GameObject>(Meteor, transform.position, Quaternion.identity);
            var forceMeteor = newMeteor.GetComponent<Rigidbody2D>();
            forceMeteor.velocity = new Vector2(UnityEngine.Random.Range(minX, maxX), -speed);
            yield return new WaitForSeconds(timeBetSpawn);
        }
    }
                    
}
