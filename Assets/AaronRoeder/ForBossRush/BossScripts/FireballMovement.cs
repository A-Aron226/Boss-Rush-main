using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AaronRoeder;

public class FireballMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeSpan = 5f;
    private bool isSpawned = false; //This is to ensure that the Original Fireball does not desawpn from hierarchy
    private Transform player;
    [SerializeField] LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent == null || transform.parent.name != "DragonTerrorBringerMesh")
        {
            isSpawned = true;
            if (isSpawned)
            {
                Destroy(gameObject, lifeSpan); //Destroys object after lifeSpan = 0;
            }
        }

        //player = GameObject.FindWithTag("Player").transform;
        Collider[] hitCol = Physics.OverlapSphere(transform.position, 100f, playerLayer); //Finding the player layer
        if (hitCol.Length > 0 )
        {
            player = hitCol[0].transform;
        }

        else
        {
            Debug.Log("Player Object not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawned)
        {
            Vector3 direction = (player.position - transform.position).normalized; //Finds the player's position
            transform.position += direction.normalized * speed * Time.deltaTime; //Fireball's movementtoward the player
        }
    }

    private void OnTriggerEnter(Collider other) //Destroys itself ince it collides with the player
    {
        if (other.CompareTag("Player"))
        {
            if (isSpawned)
            {
                Destroy(gameObject);
            }
        }
    }
}
