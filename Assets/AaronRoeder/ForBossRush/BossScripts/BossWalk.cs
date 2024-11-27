using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AaronRoeder
{
    public class BossWalk : MonoBehaviour
    {
        [SerializeField] Transform player;
        [SerializeField] float followRange = 20f;
        [SerializeField] float minRange = 5f;
        private NavMeshAgent agent;
        private Animator anim;


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }
        // Update is called once per frame
        void Update()
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position); //Getting the distance between the player and the boss

            if (distanceToPlayer <= followRange) //Checking if the player is within the followRange of the boss
            {
                agent.SetDestination(player.position);
                anim.SetBool("isWalking", true);
            }

            else if (distanceToPlayer <= minRange || distanceToPlayer > followRange)
            {
                anim.SetBool("isWalking", false);
                agent.SetDestination(transform.position);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            anim.SetFloat("speed", agent.velocity.magnitude); //setting boss speed when walking

        }
    }
}
