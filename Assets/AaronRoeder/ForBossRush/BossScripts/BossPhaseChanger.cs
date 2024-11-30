using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPhaseChanger : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    private Damageable damage;
    private bool isNextPhase = false;
    private int maxHealth;
    public float phaseDelay = 2.0f;
    public MinionSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<Damageable>();

        if (damage != null ) //grabbing events from damageable and listening to the events
        {
            damage.OnHealthChanged.AddListener(PhaseChange);
            damage.OnInitialize.AddListener(Initializing);
        }
    }

    public void Initializing(int startingHealth) //Sets serialized number to another int variable 
    {
        maxHealth = startingHealth;
    }
    public void PhaseChange(int damageAmount, int currentHealth) //Checking if boss health is less than 50% to start phase two animation
    {
        if(!isNextPhase && currentHealth <= maxHealth / 2)
        {
            isNextPhase = true;
            anim.SetTrigger("phaseTwo");
            
            if (agent != null)
            {
                agent.isStopped = true;
            }

            StartCoroutine(WaitForPhaseTwo());
        }
    }

    IEnumerator WaitForPhaseTwo() //Timer to wait until transition animation finishes then waits a little after to begin phase two
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        yield return new WaitForSeconds(phaseDelay);

        PhaseTwoStart();
    }

    void PhaseTwoStart()
    {
        if (agent != null)
        {
            agent.isStopped = false;
        }

        if (spawner != null)
        {
            spawner.EnableSpawning();
        }

        anim.SetBool("isPhaseTwo", true);
    }

    private void OnDestroy()
    {
        if (damage != null) //Removes listeners once the phase change is done
        {
            damage.OnHealthChanged.RemoveListener(PhaseChange);
            damage.OnInitialize.RemoveListener(Initializing);
        }
    }
}
