using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerNetwork : NetworkBehaviour
{
    public float moveSpeed;
    public float maxMoveRange;

    public LayerMask collisionMask;

    public GameObject bulletPrefab;

    public Text playerNameText;
    public SpriteRenderer playerSr;

    [SyncVar(hook = "OnServerStatusChanged")]
    public PlayerStatus serverStatus;
    [SyncVar(hook = "OnPlayerNameChanged")]
    public string playerName;
    [SyncVar(hook = "OnPlayerColorChanged")]
    public Color playerColor;

    private PlayerServer playerServer;
    private PlayerLocal playerLocal;
    private PlayerObserver playerObserver;

    private void Start()
    {
        serverStatus = new PlayerStatus
        {
            turnNumber = 0,
            position = transform.position,
            direction = Vector2.zero
        };

        if (isServer || isLocalPlayer)
        {
            gameObject.AddComponent<MovementController>();
        }

        if (isServer)
        {
            playerServer = gameObject.AddComponent<PlayerServer>();
        }

        if (isLocalPlayer)
        {
            playerLocal = gameObject.AddComponent<PlayerLocal>();
        }
        else if (!isServer)
        {
            playerObserver = gameObject.AddComponent<PlayerObserver>();
        }

        StartCoroutine(WaitToAddPlayer());
        OnPlayerColorChanged(playerColor);
        OnPlayerNameChanged(playerName);
    }

    IEnumerator WaitToAddPlayer()
    {
        yield return new WaitUntil(() => GameplayManager.Instance != null);

        GameplayManager.Instance.AddPlayer(this);
    }


    [Command]
    public void CmdMove(Vector2 direction)
    {
        if (playerServer != null)
        {
            serverStatus = playerServer.Move(direction);
        }
    }

    [Command]
    public void CmdFire(Vector2 cursorPos)
    {
        playerServer.Fire(cursorPos);
    }

    [ClientRpc]
    public void RpcFire(Vector2 position, Vector2 direction)
    {
        if (playerObserver != null)
        {
            playerObserver.Fire(position, direction);
        }
    }

    public void ServerOnTurnBegin()
    {
        serverStatus.remainingDistance = maxMoveRange;
    }

    [ClientRpc]
    public void RpcOnTurnBegin()
    {
        CameraFollow.Instance.target = transform;

        if (playerLocal != null)
        {
            playerLocal.OnTurnBegin();
        }
    }

    [ClientRpc]
    public void RpcDie()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        float time = 0;
        while (time < 0.4f)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(1f, 15f, Mathfx.Sinerp(0, 1, time / 0.4f));

            time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnServerStatusChanged(PlayerStatus playerStatus)
    {
        if (isLocalPlayer)
        {
            if (playerLocal != null) playerLocal.OnServerStatusChanged(playerStatus);
        }
        else if (!isServer)
        {
            if (playerObserver != null) playerObserver.OnServerPositionChanged(playerStatus);
        }
    }

    private void OnPlayerNameChanged(string newName)
    {
        playerNameText.text = newName;
    }

    private void OnPlayerColorChanged(Color newColor)
    {
        playerSr.color = newColor;
    }
}