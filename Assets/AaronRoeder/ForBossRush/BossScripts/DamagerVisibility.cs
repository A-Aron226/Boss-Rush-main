using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AaronRoeder;

public class DamagerVisibility : MonoBehaviour
{
    [SerializeField] GameObject visibilityObject;
    [SerializeField] GameObject p3VisibilityObject;
    [SerializeField] Animator anim;
    [SerializeField] string meleeName;
    [SerializeField] string clawName;
    private float visibilityTime;
    private bool isFinalPhase = false;

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

                yield return new WaitForSeconds(0.1f);
                visibilityObject.SetActive(false);


                while (anim.GetCurrentAnimatorStateInfo(0).IsName(meleeName))
                {
                    yield return null;
                }
            }

            if (stateName.IsName(clawName))
            {
                p3VisibilityObject.SetActive(true);
                Debug.Log("Visible");

                yield return new WaitForSeconds(0.13f);
                p3VisibilityObject.SetActive(false);
                Debug.Log("Invisisble");
            }

            yield return null;
        }
    }

    public void SetFinalPhase(bool value)
    {
        isFinalPhase = value;
        Debug.Log("Final Phase set to: " + value);
    }
}
