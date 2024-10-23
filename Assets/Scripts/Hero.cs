using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public int hp = 35;
    public int shields = 0;
    public TextMeshPro healthText;
    public TextMeshPro sheildText;
    void Awake()
    {
        healthText.text = hp.ToString();
    }

    public void UpdateHealth(int amount)
    {
        int remainder = amount;
        if (shields > amount)
        {
            shields += amount;
            sheildText.text = shields.ToString();
            return;
        }
        if (shields > 0)
        {
            remainder += shields;
            RemoveShields();
        }
        hp += remainder;
        healthText.text = hp.ToString();
    }
    public void UpdateShields(int amount)
    {
        shields += amount;
        sheildText.text = shields.ToString();
    }
    public void RemoveShields()
    {
        shields = 0;
        sheildText.text = "0";
    }
}
