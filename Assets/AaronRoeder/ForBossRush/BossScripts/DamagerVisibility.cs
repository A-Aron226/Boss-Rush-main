using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerVisibility : MonoBehaviour
{
    [SerializeField] GameObject visibilityObject;
    [SerializeField] Animator anim;
    [SerializeField] string meleeName;
    [SerializeField] float visibilityTime;

    private void Start()
    {
        StartCoroutine(TargetVisibility());
    }

    private IEnumerator TargetVisibility()
    {
        while (true)
        {
            var stateName = anim.GetCurrentAnimatorStateInfo(0);

            if (stateName.IsName(meleeName))
            {
                yield return new WaitForSeconds(visibilityTime);
                visibilityObject.SetActive(true);
                Debug.Log("visible");

                yield return new WaitForSeconds(0.1f);
                visibilityObject.SetActive(false);
                Debug.Log("invisible");
            }

            yield return null;
        }
    }
}
