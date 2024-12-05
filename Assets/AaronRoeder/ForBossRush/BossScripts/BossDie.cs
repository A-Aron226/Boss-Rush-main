using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossDie : MonoBehaviour
{
    [SerializeField] Collider col;
    [SerializeField] Collider sense;
    public MinionSpawner spawner;
    private Animator anim;
    private Damageable damage;
    public float desapwnDelay = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        damage = GetComponent<Damageable>();

        if (damage != null)
        {
            damage.OnDeath.AddListener(HandleDeath);
        }
    }

    public void HandleDeath()
    {
        anim.SetTrigger("isDead");

        GetComponent<NavMeshAgent>().isStopped = true;

        col.enabled = false;
        sense.enabled = false;
        spawner.StopSpawning();

        Destroy(gameObject, desapwnDelay);
    }

    private void OnDestroy()
    {
        if (damage != null)
        {
            damage.OnDeath.RemoveListener(HandleDeath);
        }
    }
}
