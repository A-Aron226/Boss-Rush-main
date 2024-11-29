using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : StateMachineBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        anim = animator;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float playerDistance = Vector3.Distance(player.position, animator.transform.position);

        if (animator.GetBool("isInMeleeRange")) //referenced from melee sensor
        {
            agent.SetDestination(animator.transform.position);
        }
        else if (playerDistance <= anim.GetFloat("followRange") && playerDistance > anim.GetFloat("minRange"))
        {
            agent.SetDestination(player.position);
            anim.SetBool("isWalking", true);
        }

        else
        {
            anim.SetBool("isWalking", false);
            agent.SetDestination(animator.transform.position);
        }

        anim.SetFloat("speed", agent.velocity.magnitude);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.SetBool("isInMeleeRange", false);
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
