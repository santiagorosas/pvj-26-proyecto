using UnityEngine;

public class EnemyDeath : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerShot>())
        {
            Destroy(gameObject);            
        }
    }
}
