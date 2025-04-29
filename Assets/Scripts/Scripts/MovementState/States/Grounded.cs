using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MovementBaseState
{
    public override void EnterState(PlayerMovement movement)
    {
        
    }

    public override void UpdateState(PlayerMovement movement)
    {
        float inputMagnitude = movement.MoveDir.magnitude;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        movement.Animator.SetFloat("Speed", inputMagnitude);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            ExitState(movement, movement.Aiming);
        }
    }


    public override void ExitState(PlayerMovement movement, MovementBaseState state)
    {
        movement.Animator.SetBool("Aiming", false);
        movement.SwitchState(state);
    }
}
