using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseChanger : MonoBehaviour
{
    private Animator anim;
    private Damageable damage;
    private bool isNextPhase = false;
    private int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        }
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
