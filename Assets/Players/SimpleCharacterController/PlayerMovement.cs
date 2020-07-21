using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;

    [Header("Movement")]
    public float walkSpeed = 6f;
    public float runMultiplier = 2f;
    public float jumpForce = 10f;
    public float gravityForce = 3f;

    [Header("Input")]
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public bool autojump = true;
    public bool moveInAir = true;

    float horizontalAxis;
    float verticalAxis;
    bool jumping;
    bool running;

    Vector3 moveDirection;
    Vector3 velocity;

    /// <summary>
    /// Assign Variables
    /// </summary>
    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Call methods
    /// </summary>
    void Update()
    {
        GetInput();
        MovePlayer();
    }

    /// <summary>
    /// Get all input for player
    /// </summary>
    void GetInput()
    {
        // move axis
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        // keycodes
        if (autojump)
            jumping = Input.GetKey(jumpKey);
        else
            jumping = Input.GetKeyDown(jumpKey);

        running = Input.GetKey(runKey);
    }

    /// <summary>
    /// With character controller, move player
    /// </summary>
    void MovePlayer()
    {
        // define input && normalize
        Vector3 input = new Vector3(horizontalAxis, 0, verticalAxis).normalized;

        // get direction
        if (cc.isGrounded)
            moveDirection = transform.TransformDirection(input) * walkSpeed;

        if (!cc.isGrounded && moveInAir)
            moveDirection = transform.TransformDirection(input) * walkSpeed;

        // speed
        if (running && cc.isGrounded)
            moveDirection *= runMultiplier;

        // call gravity before jumping
        if (!cc.isGrounded)
        {
            PlayerGravity();
        }
        else
        {
            // reset velo if grounded
            velocity.y = -1;
        }

        // jumping
        if (jumping && cc.isGrounded)
            PlayerJump();

        // move player
        cc.Move(moveDirection * Time.deltaTime);
        cc.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Gravity with acceleration over time
    /// </summary>
    void PlayerGravity()
    {
        velocity.y += Physics.gravity.y * gravityForce * Time.deltaTime;
    }

    /// <summary>
    /// Add up force for jumping
    /// </summary>
    void PlayerJump()
    {
        velocity.y = jumpForce;
    }
}
