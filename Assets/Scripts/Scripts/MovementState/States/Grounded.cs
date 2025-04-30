using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MovementBaseState
{
    // Player Movement Ki Grounded State Mein Enter Karne Wala Function
    public override void EnterState(PlayerMovement movement)
    {
        // Yahan Kuch Karna Nahi Hai Abhi
    }

    // Player Movement Ki Grounded State Ko Update Karne Wala Function
    public override void UpdateState(PlayerMovement movement)
    {
        // Input Magnitude Ko Calculate Karna (Player Ki Movement Ki Intensity)
        float inputMagnitude = movement.MoveDir.normalized.magnitude;

        // Sprinting Ki Condition Check Karna (Agar Left Shift Dabaya Hai Toh Sprinting)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (isSprinting) // Agar Player Sprint Kar Raha Hai
        {
            inputMagnitude *= 2; // Input Magnitude Ko Douna Karna (Speed Badha Do)
            movement.CurrentSpeed = movement.SprintSpeed; // Current Speed Ko Sprint Speed Pe Set Karna
        }
        else
        {
            inputMagnitude *= 0.25f; // Input Magnitude Ko Kam Karna (Walk Speed Ki Tarah)
            movement.CurrentSpeed = movement.WalkSpeed; // Current Speed Ko Walk Speed Pe Set Karna
        }

        // Animator Se Current Speed Ki Value Lena
        float currentSpeed = movement.Animator.GetFloat("Speed");

        // Smoothly Speed Ko Update Karna (Sprinting Aur Walking Ke Beech Transition Smooth Banayen)
        float smoothedSpeed = Mathf.Lerp(currentSpeed, inputMagnitude, Time.deltaTime * movement.SprintSmoothing);
        movement.Animator.SetFloat("Speed", smoothedSpeed); // Animator Ko Smooth Speed Set Karna

        // Agar Mouse Right Button Press Kiya Hai, Toh Aiming State Mein Chalay Jao
        if (Input.GetKey(KeyCode.Mouse1))
        {
            ExitState(movement, movement.Aiming); // Aiming State Mein Transition Karna
        }
    }

    // Grounded State Se Exit Karne Wala Function
    public override void ExitState(PlayerMovement movement, MovementBaseState state)
    {
        movement.Animator.SetBool("Aiming", false); // Animator Mein Aiming Bool Ko False Karna
        movement.SwitchState(state); // Naye State Pe Switch Karna
    }
}
