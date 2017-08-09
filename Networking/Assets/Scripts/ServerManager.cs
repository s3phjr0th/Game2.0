using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

public class ServerManager : MonoBehaviour
{
    private GameStates _currentGameState = GameStates.Prepare;
    public GameStates CurrentGameState
    {
        get
        {
            return _currentGameState;
        }
        set
        {
            if (value == GameStates.Player1) BeginPlayerTurn(gameplayManager.players[0]);
            if (value == GameStates.Player2) BeginPlayerTurn(gameplayManager.players[1]);
            if (value == GameStates.GameOver) GameOver();

            _currentGameState = value;
        }
    }

    private GameplayManager gameplayManager;

    private void Start()
    {
        gameplayManager = GetComponent<GameplayManager>();
        StartCoroutine(WaitForGameToBegin());
    }

    IEnumerator WaitForGameToBegin()
    {
        yield return new WaitUntil(() => gameplayManager.players.Count > 1);

        CurrentGameState = GameStates.Player1;
    }

    public void OnPlayerTurnEnded()
    {
        if (gameplayManager.players.Count <= 1)
        {
            CurrentGameState = GameStates.GameOver;
        }
        else
        {
            CurrentGameState = (CurrentGameState == GameStates.Player1) ? GameStates.Player2 : GameStates.Player1;
        }
    }

    private void BeginPlayerTurn(PlayerNetwork player)
    {
        player.ServerOnTurnBegin();
        player.RpcOnTurnBegin();
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        StartCoroutine(WaitForReturnToLobby());
    }

    IEnumerator WaitForReturnToLobby()
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<LobbyManager>().ServerReturnToLobby();
    }
}

public enum GameStates
{
    Prepare,
    Player1,
    Player2,
    GameOver
}