using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NameHealth : MonoBehaviour
{
    [SerializeField] TMP_Text bossName;
    [SerializeField] string nameOfBoss;

    // Start is called before the first frame update
    void Start()
    {
        bossName.text = nameOfBoss;
    }
}
