using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    public LayerMask collideMask;
    public LayerMask triggerMask;

    public int numRaycastHorizontal = 2;
    public int numRaycastVertical = 2;


    private float skinWidth = 0.01f;

    private BoxCollider2D bc2D;
    private Bounds colliderBounds;
    private RaycastOrigins raycastOrigins;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    private void Awake()
    {
        bc2D = GetComponent<BoxCollider2D>();
        numRaycastHorizontal = numRaycastHorizontal < 2 ? 2 : numRaycastHorizontal;
        numRaycastVertical = numRaycastVertical < 2 ? 2 : numRaycastVertical;
    }

    public PlayerStatus Move(Vector2 velocity)
    {
        PlayerStatus result = new PlayerStatus
        {
            velocity = velocity,
            isCollidingBottom = false,
            isCollidingLeft = false,
            isCollidingRight = false,
            isCollidingTop = false
        };

        UpdateColliderBounds();
        UpdateRaySpacing();

        result = RaycastHorizontal(result);
        result = RaycastVertical(result);

        return result;
    }

    private Vector2 PerformRaycasts(Vector2 origin, Vector2 velocity, Vector2 spacing, int numRaycast)
    {
        for (int i = 0; i < numRaycast; i++)
        {
            float rayLength = velocity.magnitude + skinWidth;

            Vector2 rayOrigin = origin + spacing * i;

            RaycastHit2D hit = Physics2D.Raycast(
                rayOrigin,
                velocity,
                rayLength,
                collideMask
            );

            if (hit)
            {
                velocity = velocity.ScaleTo(hit.distance - skinWidth);
            }
        }

        HashSet<RaycastHit2D> triggersCollided = new HashSet<RaycastHit2D>();

        for (int i = 0; i < numRaycast; i++)
        {
            Vector2 rayOrigin = origin + spacing * i;
            float rayLength = velocity.magnitude + skinWidth;

            // Hitting trigger colliders
            RaycastHit2D[] triggerHit = Physics2D.RaycastAll(
                rayOrigin,
                velocity,
                rayLength,
                triggerMask
            );

            foreach (RaycastHit2D hit in triggerHit)
            {
                // HashSet only adds unique items
                triggersCollided.Add(hit);
            }
        }

        foreach (RaycastHit2D hit in triggersCollided)
        {
            hit.collider.SendMessage("OnCollide", bc2D);
        }

        return velocity;
    }

    private PlayerStatus RaycastHorizontal(PlayerStatus playerStatus)
    {
        Vector2 slicedVelocity = playerStatus.velocity.WithY(0);
        Vector2 raycastOriginBottom = playerStatus.velocity.x > 0 ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        Vector2 spacing = Vector2.up * verticalRaySpacing;

        slicedVelocity = PerformRaycasts(raycastOriginBottom, slicedVelocity, spacing, numRaycastVertical);

        if(Mathf.Abs(slicedVelocity.x) < Mathf.Abs(playerStatus.velocity.x))
        {
            if (playerStatus.velocity.x > 0)
            {
                playerStatus.isCollidingRight = true;
            }
            else
            {
                playerStatus.isCollidingLeft = true;
            }
        }

        playerStatus.velocity.x = slicedVelocity.x;


        return playerStatus;
    }

    private PlayerStatus RaycastVertical(PlayerStatus playerStatus)
    {

        Vector2 slicedVelocity = playerStatus.velocity.WithX(0);
        Vector2 raycastOriginLeft = playerStatus.velocity.y > 0 ? raycastOrigins.topLeft : raycastOrigins.bottomLeft;
        Vector2 spacing = Vector2.right * horizontalRaySpacing;

        slicedVelocity = PerformRaycasts(raycastOriginLeft, slicedVelocity, spacing, numRaycastHorizontal);

        if (Mathf.Abs(slicedVelocity.y) < Mathf.Abs(playerStatus.velocity.y))
        {
            if (playerStatus.velocity.y > 0)
            {
                playerStatus.isCollidingTop = true;
            }
            else
            {
                playerStatus.isCollidingBottom = true;
            }
        }

        playerStatus.velocity.y = slicedVelocity.y;


        return playerStatus;
    }

    private void UpdateColliderBounds()
    {
        colliderBounds = bc2D.bounds;
        colliderBounds.Expand(-skinWidth * 2);
        UpdateRaycastOrigins();
    }

    private void UpdateRaycastOrigins()
    {
        raycastOrigins.topLeft = new Vector2(
            colliderBounds.min.x,
            colliderBounds.max.y
        );
        raycastOrigins.topRight = new Vector2(
            colliderBounds.max.x,
            colliderBounds.max.y
        );
        raycastOrigins.bottomLeft = new Vector2(
            colliderBounds.min.x,
            colliderBounds.min.y
        );
        raycastOrigins.bottomRight = new Vector2(
            colliderBounds.max.x,
            colliderBounds.min.y
        );
    }

    private void UpdateRaySpacing()
    {
        horizontalRaySpacing = (raycastOrigins.topRight.x - raycastOrigins.topLeft.x) / (numRaycastHorizontal - 1);
        verticalRaySpacing = (raycastOrigins.topRight.y - raycastOrigins.bottomRight.y) / (numRaycastVertical - 1);
    }
}

struct RaycastOrigins
{
    public Vector2 topLeft;
    public Vector2 topRight;
    public Vector2 bottomLeft;
    public Vector2 bottomRight;
}

public struct PlayerStatus
{
    public Vector2 velocity;
    public bool isCollidingTop;
    public bool isCollidingRight;
    public bool isCollidingBottom;
    public bool isCollidingLeft;
}