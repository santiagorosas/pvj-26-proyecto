using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public Vector2 LastNonZeroDirection => _lastNonZeroDirection;

    private float Speed => Settings.Instance.PlayerSpeed;

    private Vector2 _lastNonZeroDirection;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;

    public Vector2 Velocity { get => _velocity; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _lastNonZeroDirection = Vector2.down;
    }


    void FixedUpdate()
    {
        UpdateKeyboardInput();
        UpdateMouseInput();
    }


    private void UpdateMouseInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("click");
        }
    }

    private void UpdateKeyboardInput()
    {
        Vector3 pos = transform.position;
        
        Vector3 direction = Vector2.zero;

        if (Keyboard.current.leftArrowKey.IsPressed())
        {
            direction.x += -1;
        }
        if (Keyboard.current.rightArrowKey.IsPressed())
        {
            direction.x += 1;
        }
        if (Keyboard.current.downArrowKey.IsPressed())
        {
           direction.y += -1;
        }
        if (Keyboard.current.upArrowKey.IsPressed())
        {
            direction.y += 1;
        }

        _velocity = direction.normalized * Speed;

        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            _lastNonZeroDirection = direction;
        }
    }
}
