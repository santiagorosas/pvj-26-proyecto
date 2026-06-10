using UnityEngine;
using UnityEngine.InputSystem;

public class TransitionTest : MonoBehaviour
{
    
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("change");
            Transition.Instance.ChangeScene("Main");
        }
    }
}
