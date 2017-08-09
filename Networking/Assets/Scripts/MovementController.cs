using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    PlayerNetwork playerNetwork;

    private void Awake()
    {
        playerNetwork = GetComponent<PlayerNetwork>();
    }

    public PlayerStatus Move(PlayerStatus playerStatus)
    {
        float magnitude = Mathf.Min(playerNetwork.moveSpeed * Time.fixedDeltaTime, playerStatus.remainingDistance);
        Vector2 displacement = playerStatus.direction.normalized * magnitude;

        RaycastHit2D hit = Physics2D.Raycast(
            playerStatus.position,
            playerStatus.direction,
            magnitude + 0.01f,
            playerNetwork.collisionMask
        );

        if (hit)
        {
            displacement = displacement.ScaleTo(hit.distance - 0.01f);
        }

        return new PlayerStatus
        {
            turnNumber = playerStatus.turnNumber + 1,
            position = playerStatus.position + displacement,
            direction = playerStatus.direction,
            remainingDistance = playerStatus.remainingDistance - displacement.magnitude
        };
    }
}

[System.Serializable]
public struct PlayerStatus
{
    public int turnNumber;
    public Vector2 position;
    public Vector2 direction;
    public float remainingDistance;
}