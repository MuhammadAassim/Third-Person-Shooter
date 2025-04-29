using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MovementBaseState
{
    public override void EnterState(PlayerMovement movement)
    {
        movement.Animator.SetBool("Aiming", true);
    }

    public override void UpdateState(PlayerMovement movement)
    {
        movement.Animator.SetFloat("X", movement.Hordir);
        movement.Animator.SetFloat("Y", movement.Vertdir);

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ExitState(movement, movement.Grounded);
        }
    }


    public override void ExitState(PlayerMovement movement,MovementBaseState state)
    {
        movement.Animator.SetBool("Aiming", false);
        movement.SwitchState(state);  
    }
}
