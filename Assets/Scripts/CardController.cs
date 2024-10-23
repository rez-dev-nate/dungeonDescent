using System;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public float speed = 0f;
    private float maxSpeed = 40f;
    private float minSpeed = 8;
    public DeckController deck;
    public HandController hand;
    public DiscardController discard;
    private float distanceToTarget = 0;
    private Vector3 targetPos;
    private Vector3 faceUpRotation = new Vector3(0f, 180f, 0f);
    private Vector3 faceDownRotation = new Vector3(0f, 0f, 0f);
    private Vector3 targetRot = new Vector3(0f, 0f, 0f);
    private Vector3 newRotation;
    public cardStateEnum currentState = cardStateEnum.inDeck;
    private Boolean busy = false;
    public float cardThickness = .03f;
    public CombatManager CM;

    public enum cardStateEnum
    {
        inDeck,
        inHand,
        inDiscard,
        dragging,
        resolving,
    }

    public cardStateEnum GetState()
    {
        return currentState;
    }
    public void UpdateState(cardStateEnum newState)
    {
        currentState = newState;
    }
    void Awake()
    {
        CM = CombatManager.instance;
        hand = CM.hand;
        deck = CM.deck;
        discard = CM.discard;
        targetPos = deck.transform.position;
        targetPos.z = transform.position.z;
        transform.position = targetPos;
        transform.rotation = Quaternion.Euler(faceDownRotation);
    }


    void Update()
    {
        if (currentState == cardStateEnum.dragging) return;

        distanceToTarget = Mathf.Abs(transform.position.x - targetPos.x + transform.position.y - targetPos.y);
        if (transform.eulerAngles != targetRot)
        {
            newRotation = Vector3.RotateTowards(transform.eulerAngles, targetRot, 5 * Time.deltaTime, 10f);
            transform.rotation = Quaternion.Euler(newRotation);
        }
        if (distanceToTarget == 0) return;

        speed = (distanceToTarget * 3) + minSpeed;
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (speed < minSpeed)
        {
            speed = minSpeed;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        busy = transform.position != targetPos;
    }

    public void setTargetPosition(Vector3 target)
    {
        targetPos = target;
    }

    public void DrawCard()
    {
        currentState = cardStateEnum.inHand;
        targetRot = faceUpRotation;
        hand.DrawCard(this);
    }

    public void Discard()
    {
        currentState = cardStateEnum.inDiscard;
        hand.RemoveCard(this);
        discard.AddCard(this);
        targetRot = faceDownRotation;
    }

    private void OnMouseDown()
    {
        if (busy || cardStateEnum.inDiscard != currentState) return;


        switch (currentState)
        {
            case cardStateEnum.inDeck:
                currentState = cardStateEnum.inHand;
                targetRot = faceUpRotation;
                deck.RemoveCard(this);
                hand.DrawCard(this);

                break;
            case cardStateEnum.inHand:
                currentState = cardStateEnum.inDiscard;
                hand.RemoveCard(this);
                discard.AddCard(this);
                targetRot = faceDownRotation;
                break;
            case cardStateEnum.inDiscard:
                currentState = cardStateEnum.inDeck;
                discard.RemoveCard(this);
                deck.AddCard(this);
                targetRot = faceDownRotation;
                break;
            case cardStateEnum.dragging:
                break;
            case cardStateEnum.resolving:
                break;
            default:
                currentState = cardStateEnum.inHand;
                targetPos.z = transform.position.z;
                targetRot = faceUpRotation;
                break;
        }

    }
}