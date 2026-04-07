using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : MonoBehaviour
{
    public static event UnityAction OnDeath; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            SoundManager.Instance.PlayerDeath.Play();
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
