using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_NewInput : MonoBehaviour
{
    private PlayerControls _controls;

    void Awake()
    {
        _controls = new PlayerControls();

        //_controls.Player.Move.performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        //_controls.Player.Move.performed += HandleMovePerformed;
    }

    private void HandleMovePerformed(InputAction.CallbackContext context)
    {
        context.ReadValue<Vector2>();
    }

    void OnEnable()  { _controls.Enable(); }
    void OnDisable() { _controls.Disable(); }

    void OnMove(Vector2 dir)
    {
        //transform.Translate(dir * Time.deltaTime);
        Debug.Log("move: " + dir);
    } 

    void Update()
    {
        //Vector2 move = _controls.Player.Move.ReadValue<Vector2>();

        if (_controls.Player.Shoot.IsPressed())
        {
            Debug.Log("shoot");
        }
    }
}
