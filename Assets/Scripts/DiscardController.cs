using System.Collections.Generic;
using UnityEngine;

public class DiscardController : MonoBehaviour
{

    public CombatManager CM;

    private List<CardController> cards = new List<CardController>();
    public Vector3 screenOffset = new Vector3(-1.2f, 1.5f, 9.72f);
    private void Awake()
    {
        SetPosition();
    }
    public void AddCard(CardController card)
    {
        cards.Add(card);
        Vector3 cardPos = calcCardPosition(card);
        card.setTargetPosition(cardPos);
    }

    public void RemoveCard(CardController card)
    {
        cards.Remove(card);
    }

    public Vector3 calcCardPosition(CardController card)
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float zOffset = cards.Count * card.cardThickness;
        float z = transform.position.z + zOffset;

        return new Vector3(x, y, z);
    }

    private void SetPosition()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) + screenOffset;
        transform.position = screenPosition;
    }
}
