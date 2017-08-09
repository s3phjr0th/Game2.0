using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(PlayerController))]
public class Skill_DoubleJump : MonoBehaviour {
    private InputController inputController;
    private PlayerController playerController;

    private void Awake()
    {
        inputController = GetComponent<InputController>();
        playerController = GetComponent<PlayerController>();

        inputController.OnJumpPressed += DoubleJump;
    }

    private void OnDestroy()
    {
        inputController.OnJumpPressed -= DoubleJump;
    }

    private void DoubleJump()
    {
        if(!playerController.playerStatus.isCollidingBottom)
        {
            playerController.Jump();
        }
    }
}
