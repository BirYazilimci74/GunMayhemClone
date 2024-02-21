using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private OneWayPlatform oneWayPlatform;
    private BulletSystem bulletSystem;
    private PlayerInput playerInput;

    private void Awake()
    {
        oneWayPlatform = FindObjectOfType<OneWayPlatform>();
        bulletSystem = FindObjectOfType<BulletSystem>();
        playerInput = new PlayerInput();
        
        playerInput.Player.Enable();
        playerInput.Player.Jump.performed += JumpHandler;
        playerInput.Player.Down.performed += DownHandler;
        playerInput.Player.Shoot.performed += ShootHandler;
    }

    private void ShootHandler(InputAction.CallbackContext obj)
    {
        try
        {
            if (obj.ReadValueAsButton())
            {
                bulletSystem.Shoot();
            }
        }
        catch (NullReferenceException)
        {
            bulletSystem.gunLocked = true;
            bulletSystem.lastShootTime = Time.time;
        }
        
    }

    private void DownHandler(InputAction.CallbackContext obj)
    {
        if (obj.ReadValueAsButton() && oneWayPlatform.oneWayPlatformGameObject != null && GameManager.Instance.canPlay)
        {
            StartCoroutine(oneWayPlatform.DisableCollision());
        }
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInput.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    private void JumpHandler(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && GameManager.Instance.canPlay)
        {
            PlayerController.Instance.Jump();
        }
    }
}
