using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            if (_rigidbody == null)
            {
                throw new UnityException("No Rigidbody2D component!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector2 force = direction * Settings.Instance.PlayerForce;
        _rigidbody.AddForce(force, ForceMode2D.Force);

    }
}
