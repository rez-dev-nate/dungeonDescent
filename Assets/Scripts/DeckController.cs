using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public Transform[] cardPrefabs;
    private float zOffset = 0f;
    private float cardThickness = -.1f;

    private List<CardController> cards = new List<CardController>();
    public int handSize = 5;

    public Vector3 deckPosition = new Vector3(0, 0, -.57f);
    public CombatManager CM;

    private void Awake()
    {
        SetPosition();
    }
    private void Start()
    {
        CM.deck = this;
        foreach (Transform card in cardPrefabs)
        {
            var cardPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset);
            Transform newCard = Instantiate(card, cardPos, transform.rotation);
            cards.Add(newCard.gameObject.GetComponent<CardController>());
            zOffset += cardThickness;
        }
        StartCoroutine(DrawHand());
    }

    IEnumerator DrawHand()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < handSize; i++)
        {
            int topCard = cards.Count - 1;
            cards[topCard].DrawCard();
            cards.Remove(cards[topCard]);
            yield return new WaitForSeconds(.2f);
        }
    }

    public void AddCard(CardController card)
    {
        cards.Add(card);
        Vector3 cardPos = calcCardPosition(card);
        card.setTargetPosition(cardPos);
    }
    public Vector3 calcCardPosition(CardController card)
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float zOffset = cards.Count * card.cardThickness;
        float z = transform.position.z - zOffset;

        return new Vector3(x, y, z);
    }

    public void RemoveCard(CardController card)
    {
        cards.Remove(card);
    }

    private void SetPosition()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)) + deckPosition;
        transform.position = screenPosition;
    }

}
