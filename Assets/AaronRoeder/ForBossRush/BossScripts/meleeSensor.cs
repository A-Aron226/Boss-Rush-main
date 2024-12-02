using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeSensor : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            anim.SetBool("isInMeleeRange", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            anim.SetBool("isInMeleeRange", false);
        }
    }
}
