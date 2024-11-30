using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionFollow : StateMachineBehaviour
{
    Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        anim = animator;
        player = GameObject.FindWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);
        bool canFollow = distanceToPlayer <= anim.GetFloat("followRange") && distanceToPlayer > anim.GetFloat("minRange");

        if (canFollow) //follows player
        {
            Debug.Log("Following Player");
            agent.isStopped = false;
            agent.SetDestination(player.position);
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }

        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
            agent.isStopped = true;
            agent.ResetPath();
        }

        anim.SetFloat("speed", agent.velocity.magnitude);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
