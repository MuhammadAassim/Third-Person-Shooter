using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Rigidbody")]
    [SerializeField] private Rigidbody rb; //RigidBody Set Karo

    [Header("Animator")]
    [SerializeField] private Animator animator; //Animator Set Karo

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform; //Follow Camera Set Karo

    [Header("Player Settings")]
    [SerializeField] private float walkSpeed; //Normal Speed
    [SerializeField] private float sprintSpeed; // Bhaangnay Ki Speed
    [SerializeField] private float aimSpeed;    //Aim KI Position Mein Speed
    [SerializeField] private float aimBackSpeed; //Aim ki Position Mein Picha ki Sped 
    [SerializeField] private float rotationSpeed; //Ghoomnay ki Position Mein Picha ki Speed 

    [SerializeField] private float sprintSmoothing; //Walk aur Sprinting Ko Smooth Karnay Ki Float
    private float currentSpeed; //Woh Speed Jo Har State Kay Enter Or Exit PAr Set Hoti Hai 

    private bool rotateOnMove; //Boolean Takay Aim Rotaion aur Walk Change karay
    private MovementBaseState currentState; //Abhi Player Kiya Kar Raha 

    public Grounded Grounded = new Grounded(); //Gorunded State
    public Aiming Aiming = new Aiming(); //Aim Ki State

    public float Hordir => horDir; //Horizontal Direction Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti
    public float Vertdir => verDir; //Vertical Direction Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti
    public Animator Animator => animator; //Animator  Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti
    public float WalkSpeed => walkSpeed;  //Walk Speed  Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti
    public float SprintSpeed => sprintSpeed;  //SprintSpeed Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti
    public float AimSpeed => aimSpeed; //AimSpeed Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti
    public float AimBackSpeed => aimBackSpeed; //AimBackSpeed  Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti
    public float SprintSmoothing => sprintSmoothing; //SprintSmoothing Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti

    // yeh property currentSpeed ko bahar se access karne ke liye hai
    public float CurrentSpeed
    {
        get => currentSpeed;         // Yeh Value Return Karta Hai CurrentSpeed ki
        set => currentSpeed = value; // Yeh New Value Assign Karta Hai CurrentSpeed ko
    }


    private float horDir; //Aagahay Aur Pichay Janay Ki Direction
    private float verDir; //Baen Aur Dayen Janay Ki Direction
    private Vector3 moveDir; //Jis Direction Mein Player Move Karaga

    public Vector3 MoveDir => moveDir; //MoveDir Ka Getter Kiyo Kay Mujhay Meri Variable Public Achay Nahi Lagti

    private void Start()
    {
        SwitchState(Grounded); //Game Start Kay Waqt Pehli State
        rb = GetComponent<Rigidbody>(); //Rigid Body Component Liya
        rb.freezeRotation = true; //Rotation Freeze Ki
        currentSpeed = walkSpeed; ////Game Start Kay Waqt Pehli CurentSpeed
        Cursor.lockState = CursorLockMode.Locked; //Cursor Lock Kardiya
        Cursor.visible = false; //Cursor Ko Gayab kardiya
    }

    private void Update()
    {
        MyInputs(); //Yaha Par Inputs Liya
        currentState.UpdateState(this); // Yeh Current State Ko Update kar Raha Hai Is Object Ke Sath
        Debug.Log(currentSpeed);
    }

    public void SwitchState(MovementBaseState state) //State Change Karanay Functions
    {
        currentState = state;  // Current State Ko New State Se Replace Kar Raha Hai
        currentState.EnterState(this); // New State Ka Enter Function Call Kar Raha Hai Is Object Ke Sath
    }

    private void FixedUpdate()
    {
        HandleMovement(); //Move karany Walay Function Ki Call
        HandleRotation(); //Rotation karany Walay Function Ki Call
    }

    private void MyInputs() //Inputs Ka Function
    {
        horDir = Input.GetAxis("Horizontal"); //Horizontal Inputs
        verDir = Input.GetAxis("Vertical"); //Vertical Inputs
    }

    private void HandleMovement() //Move karany Walay Function
    {
        Vector3 moveDir = new Vector3(horDir, 0f, verDir); //Jis Direction Mein Move Kawanay Hai

        Vector3 camForward = cameraTransform.forward; // Camera Ka Forward Direction Lay Raha Hai
        Vector3 camRight = cameraTransform.right; // Camera Ka Right Direction Lay Raha Hai
        camForward.y = 0; // Forward Direction Ki Y Value Zero Kar Raha Hai
        camRight.y = 0; // Right Direction Ki Y Value Zero Kar Raha Hai
        camForward.Normalize(); // Forward Vector Ko Normalize (Yani 1 aur -1 Kar Diya) Kar Raha Hai
        camRight.Normalize(); // Right Vector Ko Normalize (Yani 1 aur -1 Kar Diya) Kar Raha Hai

        this.moveDir = camForward * moveDir.z + camRight * moveDir.x; // Final Move Direction Assign Ki

        rb.velocity = this.moveDir * currentSpeed + new Vector3(0, rb.velocity.y, 0); // Player Ko Move Karwaya
    }


    private void HandleRotation() //Rotate karany Walay Function
    {
        {
            if (currentState != Aiming) // Agar Current State Aiming Nahin Hai
            {
                Vector3 flatVelocity = rb.velocity; // Rigidbody Ki Velocity Li
                flatVelocity.y = 0; // Y Axis Ki Value Zero Ki

                if (flatVelocity.sqrMagnitude > 0.01f && rotateOnMove) // Agar Speed Thodi Zyada Hai Aur RotateOnMove True Hai
                {
                    Quaternion targetRotation = Quaternion.LookRotation(flatVelocity); // Target Rotation Calculate Kar Raha Hai Based On Movement Direction
                    rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed)); //  Rotation Apply Ki
                }
            }
        }
    }

    public void FootStep() // Footstep Sound Ya Action Ke Liye Kyo Editor Mein Error Dey Raha tha
    {
        Debug.Log("Running");
    }


    public void SetRotationOnMove(bool newRotateOnMove) // RotateOnMove Karnay Wala Function
    {
        rotateOnMove = newRotateOnMove; // Nayi Value Rotation
    }

}