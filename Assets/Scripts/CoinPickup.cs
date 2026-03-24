using Unity.VisualScripting;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision with " + other.name);
        Destroy(gameObject);
    }
}
