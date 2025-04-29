using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Rigidbody")]
    [SerializeField] private Rigidbody rb;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    [Header("Player Settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float sprintSmoothing;
    private float currentSpeed;

    private bool rotateOnMove;
    private MovementBaseState currentState;

    public Grounded Grounded = new Grounded();
    public Aiming Aiming = new Aiming();

    public float Hordir => horDir;
    public float Vertdir => verDir;
    public Animator Animator => animator;
    public float WalkSpeed => walkSpeed;
    public float SprintSpeed => sprintSpeed;
    public float SprintSmoothing => sprintSmoothing;

    public float CurrentSpeed
    {
        get => currentSpeed;
        set => currentSpeed = value;
    }


    private float horDir;
    private float verDir;
    private Vector3 moveDir;

    public Vector3 MoveDir => moveDir;

    private void Start()
    {
        SwitchState(Grounded);
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MyInputs();
        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void MyInputs()
    {
        horDir = Input.GetAxis("Horizontal");
        verDir = Input.GetAxis("Vertical");
    }

    private void HandleMovement()
    {
        Vector3 moveDir = new Vector3(horDir, 0f, verDir);

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        this.moveDir = camForward * moveDir.z + camRight * moveDir.x;

        rb.velocity = this.moveDir * currentSpeed + new Vector3(0, rb.velocity.y, 0);
    }


    private void HandleRotation()
    {
        if (currentState != Aiming)
        {
            Vector3 flatVelocity = rb.velocity;
            flatVelocity.y = 0;

            if (flatVelocity.sqrMagnitude > 0.01f && rotateOnMove)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatVelocity);
                rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed));
            }
        }
    }

    public void FootStep()
    {
        //Debug.Log("Running");
    }


    public void SetRotationOnMove(bool newRotateOnMove)
    {
        rotateOnMove = newRotateOnMove;
    } 

}