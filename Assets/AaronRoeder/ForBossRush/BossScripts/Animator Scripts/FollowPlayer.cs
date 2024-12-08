using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AaronRoeder
{
    public class FollowPlayer : StateMachineBehaviour
    {
        Transform player;
        private NavMeshAgent agent;
        private Animator anim;
        [SerializeField] LayerMask playerLayer;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent = animator.GetComponent<NavMeshAgent>();
            anim = animator;
            //player = GameObject.FindWithTag("Player").transform;
            Collider[] hitCol = Physics.OverlapSphere(animator.transform.position, 300f, playerLayer);

            if (hitCol.Length > 0 )
            {
                player = hitCol[0].transform;
            }

            else
            {
                Debug.Log("Player Object not found!");
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            /*if (anim.GetBool("isPhaseTwo") || anim.GetBool("isFinalPhase"))
            {
                return;
            }*/

            float distanceToPlayer = Vector3.Distance(player.position, animator.transform.position);

            if (distanceToPlayer <= anim.GetFloat("followRange")) //follows player
            {
                agent.SetDestination(player.position);
                anim.SetBool("isWalking", true);
                anim.SetBool("canShoot", false);

                //An Attempt for the boss to look at the player
                Vector3 direction = (player.position - animator.transform.position).normalized;
                Quaternion look = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, look, Time.deltaTime * agent.angularSpeed);
            }

            else if (distanceToPlayer <= anim.GetFloat("minRange") || distanceToPlayer > anim.GetFloat("followRange"))
            {
                anim.SetBool("isWalking", false);
                agent.SetDestination(animator.transform.position);
                anim.SetBool("canShoot", true);
            }

            else
            {
                anim.SetBool("isWalking", false);
            }

            anim.SetFloat("speed", agent.velocity.magnitude);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        /*override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }*/

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
}
