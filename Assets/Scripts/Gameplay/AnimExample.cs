using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AnimExample : MonoBehaviour
{
    private enum AnimationName
    {
        Idle = 1,
        Walk = 2,
        Attack = 3
    }

    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();

        SceneManager.LoadScene("Main");

        Transition.Instance.ChangeScene("Main");
        Transition.OnSceneLoaded += HandleSceneLoaded;
    }

    private void HandleSceneLoaded()
    {
        Transition.OnSceneLoaded -= HandleSceneLoaded;
    }

    private void SetAnimState(AnimationName name)
    {
        _animator.SetInteger("State", (int)name);
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SetAnimState(AnimationName.Idle);
            //_animator.SetInteger("State", (int)AnimationName.Idle);
        }
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            _animator.SetInteger("State", 2);
        }
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            _animator.SetInteger("State", 3);
        }
    }

    void OnDestroy()
    {
        Transition.OnSceneLoaded -= HandleSceneLoaded;
    }
}
