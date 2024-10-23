using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    GameManager _gameManager;

    public Vector3 screenOffset = new Vector3(-1.2f, 3.5f, 9.72f);

    void Start()
    {
        _gameManager = GameManager.instance;
    }

    void OnMouseUp()
    {
        // if (_gameManager.getGameState() != GameManager.GameStateEnum.PlayerTurn) return;

        // _gameManager.ChangeGameState(GameManager.GameStateEnum.EndTurn);
    }

    void Update()
    {
        SetPosition();
    }
    private void SetPosition()
    {
        Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) + screenOffset;
        transform.position = screenPosition;
    }
}
