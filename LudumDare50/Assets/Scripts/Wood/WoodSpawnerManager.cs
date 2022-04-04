using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpawnerManager : MonoBehaviour
{
    public GameObject woodPrefab;

    public int totalLogsSpawned = 0;

    public int currentLogs;

    public float elapsedTime = 0;

    void Start()
    {
        currentLogs = transform.childCount;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;


        if(currentLogs < 105)
        {
            SpawnWood();
        }

    }

    private void SpawnWood()
    {
        float lowerLimit = 4 + totalLogsSpawned / 4;
        float upperLimit = 7 + totalLogsSpawned / 3;
        

        Vector3 pos = Random.onUnitSphere;
        pos.z = 0;
        pos = pos.normalized;

        Instantiate(woodPrefab, pos * Random.Range(lowerLimit, upperLimit), Quaternion.identity, transform);
        totalLogsSpawned++;
        currentLogs++;
    }
}
