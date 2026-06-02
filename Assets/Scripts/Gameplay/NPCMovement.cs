using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        _animator.SetTrigger("Walk");
    }

    
}
