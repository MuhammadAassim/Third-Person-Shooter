using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MovementBaseState
{
    // Aiming State Mein Enter Karte Waqt, Animator Mein Aiming Bool Ko True Karna
    public override void EnterState(PlayerMovement movement)
    {
        movement.Animator.SetBool("Aiming", true); // Aiming Bool Ko True Karna
    }

    // Aiming State Ko Update Karne Wala Function
    public override void UpdateState(PlayerMovement movement)
    {
        // Horizontal Aur Vertical Direction Ko Animator Mein Set Karna
        movement.Animator.SetFloat("X", movement.Hordir); // X Direction
        movement.Animator.SetFloat("Y", movement.Vertdir); // Y Direction

        // Agar Mouse Ka Right Button Release Kiya Hai Toh Grounded State Mein Wapas Jao
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ExitState(movement, movement.Grounded); // Grounded State Mein Switch Karna
        }

        // Agar Vertical Direction 0 Se Kam Hai Toh AimBackSpeed Set Karna
        if (movement.Vertdir <= 0f)
        {
            movement.CurrentSpeed = movement.AimBackSpeed; // Pichay Ki Speed Ko Set Karna
        }
        else
        {
            movement.CurrentSpeed = movement.AimSpeed; // Aim Ki Speed Ko Set Karna
        }
    }

    // Aiming State Se Exit Karte Waqt Animator Ko False Karna Aur Naye State Mein Switch Karna
    public override void ExitState(PlayerMovement movement, MovementBaseState state)
    {
        movement.Animator.SetBool("Aiming", false); // Aiming Bool Ko False Karna
        movement.SwitchState(state); // Naye State Pe Switch Karna (Grounded State)
    }
}
