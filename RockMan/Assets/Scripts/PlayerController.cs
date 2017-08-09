using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;

    [Space]
    public float dashMultiplier;
    public float dashTime;

    public bool isDashing { get; private set; }


    private InputController inputController;
    private Controller2D controller2D;
    private Vector2 velocity;
    private int faceDirection;

    public PlayerStatus playerStatus { get; private set; }

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        controller2D = GetComponent<Controller2D>();

        inputController.OnMovePressed += Move;
        inputController.OnJumpPressed += JumpIfPossible;
        inputController.OnDashPressed += DashIfPossible;
    }

    private void OnDestroy()
    {
        inputController.OnMovePressed -= Move;
        inputController.OnJumpPressed -= JumpIfPossible;
        inputController.OnDashPressed -= DashIfPossible;
    }

    public void FixedUpdate()
    {
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        playerStatus = controller2D.Move(velocity * Time.fixedDeltaTime);

        transform.position += (Vector3)playerStatus.velocity;

        if (playerStatus.isCollidingBottom || playerStatus.isCollidingTop)
        {
            velocity.y = 0;
        }
        //anim.SetFloat("MoveHorizontal", Input.GetAxisRaw(nameHorizontal));
        Debug.Log(playerStatus.isCollidingBottom);
        //if (!playerStatus.isCollidingBottom)
        //{
        //    anim.SetTrigger("Jump");
        //}
        //else if (playerStatus.isCollidingBottom)
        //{
        //    anim.SetTrigger("Idle");
        //}
    }


    public void JumpIfPossible()
    {
        if (playerStatus.isCollidingBottom)
        {
            Jump();
        }
    }
    public void Jump()
    {
        velocity.y = jumpSpeed;
    }


    private void DashIfPossible()
    {
        if (!isDashing && playerStatus.isCollidingBottom)
        {
            Dash();
        }
    }
    public void Dash()
    {
        velocity.x = faceDirection * movementSpeed * dashMultiplier;
        isDashing = true;

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }

    private void Move(float direction)
    {
        if (!isDashing)
        {
            if (direction != 0)
            {
                faceDirection = (int)Mathf.Sign(direction);
            }

            velocity.x = direction * movementSpeed;
        }
    }
}
