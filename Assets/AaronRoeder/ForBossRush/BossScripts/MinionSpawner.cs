using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    [SerializeField] GameObject minion;
    [SerializeField] BoxCollider spawner;
    public float spawnDelay = 3.0f;
    private bool startSpawining = false;

    // Start is called before the first frame update
    void Start()
    {
        startSpawining = false;
    }

    public void EnableSpawning()
    {
        startSpawining = true;
        StartCoroutine(MinionSpawn());
    }

    IEnumerator MinionSpawn()
    {
        while (startSpawining)
        {
            yield return new WaitForSeconds(spawnDelay);

            Vector3 randomPosition = GetRandomPosition();

            Instantiate(minion, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 minVolume = spawner.bounds.min;
        Vector3 maxVolume = spawner.bounds.max;

        float randomX = Random.Range(minVolume.x, maxVolume.x);
        float randomY = Random.Range(minVolume.y, maxVolume.y);
        float randomZ = Random.Range(minVolume.z, maxVolume.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    public void DisableMinionSpawn()
    {
        startSpawining = false;
        StopCoroutine(MinionSpawn());
    }
}
