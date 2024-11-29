using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class RangeAttack : StateMachineBehaviour
{
    public string fireballName = "Fireball"; //fireball prefab reference
    private GameObject fireball;
    public string headName = "Head"; //Will reference boss' head transform position
    private Transform head;
    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    //private float fireTime = 0f;
    //public float fireRate = 1.5f;
    public float spawnDelay = 0.5f; //delay timer for fireball
    private bool hasSpawned = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        anim = animator;

        fireball = GameObject.Find(fireballName); //Looks for a game object in hierarchy with string name
        /*if (fireball == null)
        {
            Debug.Log("Fireball Object not found");
        }*/

        head = FindChildHead(animator.transform, headName); //Looks for head in hierarchy with string name using recursive function
        /*if (head == null)
        {
            Debug.Log("Head object not found");
        }*/

        anim.SetFloat("spawnDelay", spawnDelay);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float playerDistance = Vector3.Distance(player.position, animator.transform.position);

        if (playerDistance <= anim.GetFloat("followRange"))
        {
            anim.SetBool("canShoot", false);
        }

        else
        {
            anim.SetBool("canShoot", true);

            if (!hasSpawned && stateInfo.normalizedTime >= spawnDelay && stateInfo.normalizedTime < 1.0f)
            {
                Projectile(anim);
                hasSpawned = true;
            }

            /*if (Time.time >= fireTime)
            {
                //Projectile();
                fireTime = Time.time + fireRate;
            }*/


        }
    }

    private void Projectile(Animator animator)
    {
        if (fireball != null && head != null)
        {
            Instantiate(fireball, head.position, head.rotation);
        }

        /*else
        {
            if (fireball == null)
            {
                Debug.Log("Firebal is null!");
            }

            if (head == null)
            {
                Debug.Log("Head is null!");
            }
        }*/
        
    }

    private Transform FindChildHead(Transform parent, string name) //Had to figure out and implement a recursive function to have the script find the head object that was deeply nested
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child;
            }

            Transform childFound = FindChildHead(child, name);

            if (childFound != null)
            {
                return childFound;
            }
        }
        return null;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim.SetFloat("spawnDelay", 0);
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
