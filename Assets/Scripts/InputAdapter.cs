using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAdapter : MonoBehaviour
{
    private PlayerControls _controls;
    [SerializeField] private PlayerMovementSystem _playerMovement;

    private void Awake()
    {
        _controls = new PlayerControls();

        _controls.Player.MovementDirection.performed += ctx => _playerMovement.Move(ctx.ReadValue<Vector2>());
        _controls.Player.MovementDirection.canceled += ctx => _playerMovement.Move(ctx.ReadValue<Vector2>());

        _controls.Player.Shoot.performed += ctx => _playerMovement.Shoot();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
