using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocal : MonoBehaviour
{
    private PlayerNetwork playerNetwork;
    private MovementController movementController;

    private Vector2 velocity = Vector2.zero;
    private PlayerStatus predictedStatus;

    private LinkedList<PlayerStatus> history = new LinkedList<PlayerStatus>();

    void Awake()
    {
        playerNetwork = GetComponent<PlayerNetwork>();
        movementController = GetComponent<MovementController>();

        predictedStatus = new PlayerStatus
        {
            turnNumber = 0,
            position = transform.position,
            direction = velocity
        };
    }

    public void OnTurnBegin()
    {
        InputController.Instance.OnMove += OnMove;
        InputController.Instance.OnFire += OnFire;
        predictedStatus.remainingDistance = playerNetwork.maxMoveRange;
    }

    public void OnTurnEnd()
    {
        InputController.Instance.OnMove -= OnMove;
        InputController.Instance.OnFire -= OnFire;
        velocity = Vector2.zero;
    }

    private void OnMove(Vector2 direction)
    {
        velocity = direction.normalized;
    }

    private void OnFire(Vector2 cursorPos)
    {
        // Fire locally
        if (!playerNetwork.isServer)
        {
            BulletController.CreateBullet(
                playerNetwork.bulletPrefab,
                transform.position,
                cursorPos - (Vector2)transform.position
            );
        }

        // TODO CmdFire to Server
        playerNetwork.CmdFire(cursorPos);

        OnTurnEnd();
    }

    private void FixedUpdate()
    {
        // Predict the result locally
        predictedStatus.direction = velocity;
        predictedStatus = movementController.Move(predictedStatus);
        transform.position = predictedStatus.position;

        history.AddLast(predictedStatus);

        // Send move command to server
        playerNetwork.CmdMove(velocity);
    }

    public void OnServerStatusChanged(PlayerStatus serverStatus)
    {
        if (history.First.Value.turnNumber > serverStatus.turnNumber)
            return;

        while (history.Count > 0 &&
            history.First.Value.turnNumber < serverStatus.turnNumber)
        {
            history.RemoveFirst();
        }
        history.First.Value = serverStatus;

        PlayerStatus currentStatus = history.First.Value;
        history.RemoveFirst();

        foreach (PlayerStatus status in history)
        {
            currentStatus.direction = status.direction;
            currentStatus = movementController.Move(currentStatus);
        }

        predictedStatus = currentStatus;
        transform.position = predictedStatus.position;
    }
}