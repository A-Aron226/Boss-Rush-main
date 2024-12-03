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
    private bool isFinalPhase = false;
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
    public void PhaseChange(int damageAmount, int currentHealth) 
    {
        Debug.Log("PhaseChange with current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Boss is dead"); //PlaceHolder to put in boss death animation
            return;
        }

        if(!isNextPhase && currentHealth <= maxHealth / 2) //Checking if boss health is less than 50% to start phase two animation
        {
            if (!anim.GetBool("isPhaseTwo"))
            {
                PhaseTwoTrigger();
            }
        }

        else if (!isFinalPhase && currentHealth <= maxHealth / 3) //Checking if boss health is less than 25% to start phase three animation
        {
            if (!anim.GetBool("isFinalPhase"))
            {
                PhaseThreeTrigger();
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void PhaseTwoTrigger()
    {
        isNextPhase = true;
        anim.SetTrigger("phaseTwo");
        Debug.Log("Phase two triggered");

        if (agent != null)
        {
            agent.isStopped = true;
        }

        StartCoroutine(WaitForPhaseTwo());
    }

    IEnumerator WaitForPhaseTwo() //Timer to wait until transition animation finishes then waits a little after to begin phase two
    {
        yield return null;
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        while(state.IsName("Hit"))
        {
            yield return null;
            state = anim.GetCurrentAnimatorStateInfo(0);
        }

        yield return new WaitForSeconds(state.length + phaseDelay);

        PhaseTwoStart();
    }

    void PhaseTwoStart() //Activates the minion spawner
    {
        if (agent != null)
        {
            agent.isStopped = false;
            agent.ResetPath();
        }

        if (spawner != null)
        {
            spawner.EnableSpawning();
        }

        anim.SetBool("isPhaseTwo", true);
        Debug.Log("Phase two started");
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    private void PhaseThreeTrigger()
    {
        isFinalPhase = true;
        anim.SetTrigger("phaseThree");
        Debug.Log("Phase 3 trigger set");

        if (agent != null)
        {
            agent.isStopped = true;
        }

        StartCoroutine(WaitForPhaseThree());
    }

    IEnumerator WaitForPhaseThree() //Timer to wait until transition animation finishes then waits a little after to begin phase three
    {
        yield return null;
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        while (state.IsName("Hit"))
        {
            Debug.Log("Current State: " + anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            yield return null;
            state = anim.GetCurrentAnimatorStateInfo(0);
        }

        Debug.Log("Phase 3 transition playing");

        yield return new WaitForSeconds(state.length + phaseDelay);

        PhaseThreeStart();
    }

    void PhaseThreeStart()
    {
        if (agent != null)
        {
            agent.isStopped = false;
            agent.ResetPath();
            Debug.Log("Boss resuming movement");
        }

        if (spawner != null)
        {
            spawner.spawnDelay = spawner.spawnDelay / 2;
        }

        anim.SetBool("isFinalPhase", true);
        Debug.Log("Phase 3 started");

        var damagerVisibility = GetComponent<DamagerVisibility>();
        if (damagerVisibility != null)
        {
            damagerVisibility.SetFinalPhase(true);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    private void OnDestroy()
    {
        if (damage != null) //Removes listeners
        {
            damage.OnHealthChanged.RemoveListener(PhaseChange);
            damage.OnInitialize.RemoveListener(Initializing);
        }
    }
}
