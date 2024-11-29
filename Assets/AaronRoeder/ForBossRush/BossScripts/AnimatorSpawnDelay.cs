using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSpawnDelay : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] FireballSpawner spawner;

    // Update is called once per frame
    void Update()
    {
        float spawnDelay = anim.GetFloat("spawnDelay");
        if (spawnDelay > 0)
        {
            spawner.StartSpawning(spawnDelay);
            anim.SetFloat("spawnDelay", 0);
        }
    }
}
