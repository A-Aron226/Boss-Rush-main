using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Child : MonoBehaviour
{
    [SerializeField] Transform parent;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, parent.position, Time.deltaTime * 5);
        transform.rotation = Quaternion.Slerp(transform.rotation, parent.rotation, Time.deltaTime * 5);
    }
}
