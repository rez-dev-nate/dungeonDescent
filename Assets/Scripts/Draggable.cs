using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 mousePositionOffset;
    public CardController card;
    public Vector3 handPosition;

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Start()
    {
        handPosition = card.CM.hand.transform.position;
    }
    private void OnMouseDown()
    {
        if (card.GetState() != CardController.cardStateEnum.inHand) return;
        // startingPosition = transform.position;

        card.UpdateState(CardController.cardStateEnum.dragging);
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (card.GetState() != CardController.cardStateEnum.dragging) return;

        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    private void OnMouseUp()
    {
        if (card.GetState() != CardController.cardStateEnum.dragging) return;

        bool onBattlefield = Mathf.Abs(handPosition.y - transform.position.y) > 2;

        if (onBattlefield)
        {
            playCard();
            return;
        }

        returnCardToHand();
    }
    void returnCardToHand() => card.UpdateState(CardController.cardStateEnum.inHand);
    void playCard() => card.Discard();
}