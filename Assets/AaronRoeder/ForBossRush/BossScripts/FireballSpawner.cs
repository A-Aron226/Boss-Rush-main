using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AaronRoeder;

public class FireballSpawner : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    [SerializeField] Transform spawnPoint;

    // Start is called before the first frame update
    public void StartSpawning(float delay)
    {
        StartCoroutine(ProjectileDelay(delay));
    }

    private IEnumerator ProjectileDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (fireball != null && spawnPoint != null)
        {
            Instantiate(fireball, spawnPoint.position, spawnPoint.rotation);
        }

        else if (spawnPoint == null)
        {
            Debug.Log("No Spawn point assigned here!");
        }

        else if (fireball == null)
        {
            Debug.Log("No fireball object assigned here!");
        }
    }
}
