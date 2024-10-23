using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    private float xOffset = .5f;
    public CombatManager CM;

    public Vector3 screenOffset = new Vector3(0, 1.5f, 9.72f);

    private List<CardController> cards = new List<CardController>();

    private void Awake()
    {
        SetPosition();
    }
    public void DrawCard(CardController card)
    {
        cards.Add(card);
        UpdateCardPositions();
    }

    public void RemoveCard(CardController card)
    {
        cards.Remove(card);
        UpdateCardPositions();
    }

    private void UpdateCardPositions()
    {
        if (cards.Count == 0) return;

        for (int i = 0; i < cards.Count; i++)
        {
            calcHandPosition(i, cards[i]);
        }
    }

    public void EndTurn()
    {
        StartCoroutine(RemoveAllCards());
    }
    public IEnumerator RemoveAllCards()
    {
        int cardsInHand = cards.Count;
        for (int i = 0; i < cardsInHand; i++)
        {
            CM.discard.AddCard(cards[0]);
            cards.Remove(cards[0]);
            UpdateCardPositions();
            yield return new WaitForSeconds(.1f);
        }
    }


    public void calcHandPosition(int index, CardController card)
    {
        int totalCards = cards.Count;
        float center = totalCards / 2;
        float x = (index - center) * -xOffset;
        float zOffset = index * card.cardThickness;
        float z = transform.position.z + zOffset;
        float y = transform.position.y;

        card.setTargetPosition(new Vector3(x, y, z));
    }
    private void SetPosition()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)) + screenOffset;
        transform.position = screenPosition;
    }
}
