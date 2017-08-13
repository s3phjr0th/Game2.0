using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour {
	public float movementSpeed;
	public float jumpSpeed;

	[Space]
	public float dashMultiplier;
	public float dashTime;
    public CharacterSkill characterSkill;
    
	public bool isDashing { get; private set; }
    public bool canDashing;
    public bool canDoubleJump;


    private Animator anim;


	private InputController inputController;
	private Controller2D controller2D;
	private Vector2 velocity;
	private int faceDirection;

	public PlayerStatus playerStatus { get; private set; }

	private void Awake()
	{
        canDashing = false;
        canDoubleJump = false;
        inputController = GetComponent<InputController>();
		controller2D = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        characterSkill = GetComponent<CharacterSkill>();

		inputController.OnMovePressed += Move;
		inputController.OnJumpPressed += JumpIfPossible;
		inputController.OnDashPressed += DashIfPossible;
        anim.SetBool("Jump", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Fall", false);
        characterSkill.Skill1();
        characterSkill.Skill2();
    }
    private void Start()
    {
        Debug.Log(characterSkill.player1Character);
        Debug.Log(characterSkill.player2Character);
    }
    private void OnDestroy()
	{
		inputController.OnMovePressed -= Move;
		inputController.OnJumpPressed -= JumpIfPossible;
		inputController.OnDashPressed -= DashIfPossible;
	}

	public void FixedUpdate()
	{
        anim.SetBool("Jump", false);
        anim.SetBool("Idle", false);
        anim.SetBool("Fall", false);

        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

		playerStatus = controller2D.Move(velocity * Time.fixedDeltaTime);

		transform.position += (Vector3)playerStatus.velocity;

		if (playerStatus.isCollidingBottom || playerStatus.isCollidingTop)
		{
			velocity.y = 0;
		}
        if (Input.GetAxisRaw(inputController.nameHorizontal) == 0)
        {
            anim.SetFloat("MoveRight", 0);
            anim.SetFloat("MoveLeft", 0);

        }
        //anim.SetFloat("MoveHorizontal", Input.GetAxisRaw(nameHorizontal));
        if (playerStatus.isCollidingBottom && Input.GetAxisRaw(inputController.nameHorizontal) !=0)
        {
            if (Input.GetAxisRaw(inputController.nameHorizontal)>0)
            {
                anim.SetFloat("MoveRight", Input.GetAxisRaw(inputController.nameHorizontal));
                anim.SetFloat("MoveLeft", 0);

            }
            if (Input.GetAxisRaw(inputController.nameHorizontal) < 0)
            {
                anim.SetFloat("MoveRight",0);
                anim.SetFloat("MoveLeft", Input.GetAxisRaw(inputController.nameHorizontal));
            }
        }
        else if (anim.GetFloat("MoveRight")==0 && anim.GetFloat("MoveLeft")==0 && playerStatus.isCollidingBottom)
        {
            anim.SetBool("Idle", playerStatus.isCollidingBottom);
        }
        else if (!playerStatus.isCollidingBottom && playerStatus.velocity.y > 0)
        {
            anim.SetBool("Jump", !playerStatus.isCollidingBottom);
        }
        else if (!playerStatus.isCollidingBottom && playerStatus.velocity.y < -5)
        {
            anim.SetBool("Fall", !playerStatus.isCollidingBottom);
        }
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
