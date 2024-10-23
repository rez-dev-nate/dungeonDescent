using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 35;
    public TextMeshPro healthText;
    void Awake()
    {
        healthText.text = HP.ToString();
    }

    public void UpdateHealth(int amount)
    {
        HP += amount;
        healthText.text = HP.ToString();
    }
}
