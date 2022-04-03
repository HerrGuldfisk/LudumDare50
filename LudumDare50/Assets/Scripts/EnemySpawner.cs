using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 5f;
    [SerializeField] float minDistanceFromPlayer = 10f;
    [SerializeField] float maxDistanceFromPlayer = 20f;

    [SerializeField] List<GameObject> EnemiesToSpawn = new List<GameObject>();
    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("Spawn", spawnDelay, spawnDelay);
    }

    void Spawn()
    {
        if (!player) return;

        float distanceFromPlayer = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        Vector2 vecFromPlayer = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * distanceFromPlayer;
        Vector2 spawnPos = (Vector2)player.position + vecFromPlayer;
        
        int randomPick = (int)Random.Range(0,EnemiesToSpawn.Count);
        GameObject.Instantiate(EnemiesToSpawn[randomPick], spawnPos, Quaternion.identity);
    }
}
