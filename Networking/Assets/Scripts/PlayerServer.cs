using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServer : MonoBehaviour {
    private PlayerNetwork playerNetwork;
    private MovementController movementController;

    private void Awake()
    {
        playerNetwork = GetComponent<PlayerNetwork>();
        movementController = GetComponent<MovementController>();
    }

    public PlayerStatus Move(Vector2 direction)
    {
        PlayerStatus serverStatus = playerNetwork.serverStatus;
        serverStatus.direction = direction;
        serverStatus = movementController.Move(serverStatus);

        transform.position = serverStatus.position;
        return serverStatus;
    }

    public void Fire(Vector2 cursorPos)
    {
        BulletController.CreateBullet(
            playerNetwork.bulletPrefab,
            transform.position,
            cursorPos - (Vector2)transform.position,
            playerNetwork
        );

        playerNetwork.RpcFire(transform.position, cursorPos - (Vector2)transform.position);
    }
}
