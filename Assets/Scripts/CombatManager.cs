using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public static CombatManager instance;
    public DeckController deck;
    public HandController hand;
    public DiscardController discard;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {

    }
}
