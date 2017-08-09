using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour {
    public KeyCode jumpButton;
    public KeyCode dashButton;

    public Action<float> OnMovePressed;
    public Action OnJumpPressed;
    public Action OnDashPressed;

    private void Update()
    {
        if(OnMovePressed != null)
        {
            OnMovePressed(Input.GetAxisRaw("Horizontal"));
        }

        if (OnJumpPressed != null && Input.GetKeyDown(jumpButton))
        {
            OnJumpPressed();
        }

        if(OnDashPressed != null && Input.GetKeyDown(dashButton))
        {
            OnDashPressed();
        }
    }
}
