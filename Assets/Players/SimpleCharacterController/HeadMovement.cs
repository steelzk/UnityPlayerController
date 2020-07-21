using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    [Header("Sensitivity")]
    public float xSpeed = 130f;
    public float ySpeed = 130f;

    Transform playerParent;
    float xMouse;
    float yMouse;

    float xRotation;

    /// <summary>
    /// Get variables
    /// </summary>
    void Awake()
    {
        playerParent = transform.parent;
    }

    void Update()
    {
        GetInput();
        Look();
    }

    /// <summary>
    /// Get player input for rotation camera/player
    /// </summary>
    void GetInput()
    {
        xMouse = Input.GetAxisRaw("Mouse X") * xSpeed * Time.deltaTime;
        yMouse = Input.GetAxisRaw("Mouse Y") * ySpeed * Time.deltaTime;
    }
    
    /// <summary>
    /// Look around with input defined above
    /// </summary>
    void Look()
    {
        // clamp x rotation
        xRotation -= yMouse;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // set rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerParent.Rotate(Vector3.up * xMouse);
    }
}
