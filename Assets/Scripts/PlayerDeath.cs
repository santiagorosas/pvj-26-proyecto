using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    public static event UnityAction OnDeath; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
