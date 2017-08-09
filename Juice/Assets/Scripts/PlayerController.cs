using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Controller2D))]
public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public LayerMask enemyLayerMask;

    private Animator anim;
    private Controller2D controller2D;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller2D = GetComponent<Controller2D>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Hammer");
        }

        Vector2 velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).ScaleTo(moveSpeed);

        PlayerStatus playerStatus = controller2D.Move(velocity * Time.deltaTime);
        transform.position += (Vector3) playerStatus.velocity;

        anim.SetFloat("MoveVertical", Input.GetAxisRaw("Vertical"));
	}

    public void Smash(int dir)
    {
        Vector2 direction = Vector2.zero;
        switch (dir)
        {
            case 0:
                direction = Vector2.up;
                break;
            case 1:
                direction = Vector2.right;
                break;
            case 2:
                direction = Vector2.down;
                break;
            case 3:
                direction = Vector2.left;
                break;
            default:
                direction = Vector2.zero;
                break;
        }

        Collider2D[] chickens = Physics2D.OverlapCircleAll((Vector2)transform.position + direction * 16, 8, enemyLayerMask);
        foreach (Collider2D chickenCollider in chickens)
        {
            DestroyObject(chickenCollider.gameObject);
        }
    }
}
