using UnityEngine;
using UnityEngine.InputSystem;

public class AnimExample : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("1");
            _animator.SetInteger("State", 1);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Debug.Log("2");
            _animator.SetInteger("State", 2);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            Debug.Log("3");
            _animator.SetInteger("State", 3);
        }
    }
}
