using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAtPlayer : StateMachineBehaviour
{
    [SerializeField] float speedMultiplier = 2.0f;
    Transform player;
    private NavMeshAgent agent;
    private Animator anim;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        anim = animator;
        player = GameObject.FindWithTag("Player").transform;

        if (agent != null) //Sets the movement speed to a run speed
        {
            agent.speed *= speedMultiplier;
            Debug.Log("Boss Speed set to Running: " + agent.speed);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceToPlayer <= anim.GetFloat("followRange")) //follows player
        {
            agent.SetDestination(player.position);
            anim.SetBool("isWalking", true);
            Debug.Log("Following Player");
        }

        else if (distanceToPlayer <= anim.GetFloat("minRange") || distanceToPlayer > anim.GetFloat("followRange"))
        {
            anim.SetBool("isWalking", false);
            agent.SetDestination(animator.transform.position);
            Debug.Log("Player out of range, boss going idle");
        }

        else
        {
            anim.SetBool("isWalking", false);
            Debug.Log("Idle State, boss unable to move");
        }

        anim.SetFloat("speed", agent.velocity.magnitude);
        Debug.Log("Animator speed set to: " + agent.velocity.magnitude);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent != null) //Resets speed back to walking speed
        {
            agent.speed /= speedMultiplier;
            Debug.Log("Boss speed has reset back to Normal: " + agent.speed);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
