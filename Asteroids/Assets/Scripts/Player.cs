using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] Camera mainCamera;
    private Vector3 moveDirection;
    private Vector2 mousePos;

    void Start()
    {
        // Vector3 newAngle = new(0, 0, -90f);
        // transform.eulerAngles = newAngle;
    }

    void Update()
    {
        rigidBody.linearVelocity = new Vector2(moveDirection.x * playerSpeed, moveDirection.y * playerSpeed);

        //Calculate Mouse position and rotate Player sprite to look at the mouse
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        Vector3 rotateDirection = (worldPosition - transform.position).normalized;
        rotateDirection.z = 0;

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            mousePos = context.ReadValue<Vector2>();
        }
    }
}
