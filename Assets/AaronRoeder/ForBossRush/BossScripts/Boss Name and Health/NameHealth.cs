using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NameHealth : MonoBehaviour
{
    [SerializeField] TMP_Text bossName;
    [SerializeField] int maxHealth;
    int currHealth;
    public UnityEvent<int> OnInitialize;
    public UnityEvent<int, int> OnHealthChanged;
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        bossName.text = "Morgar, the Warlord";
        OnInitialize?.Invoke(maxHealth);
        OnHealthChanged?.Invoke(maxHealth, maxHealth);
    }
}
