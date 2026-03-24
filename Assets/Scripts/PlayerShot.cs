using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private Vector2 _direction;

    public void Init(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    void Update()
    {
        Vector3 velocity = _direction * Settings.Instance.PlayerShotSpeed;
        transform.position += velocity * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyChase>() != null)
        {
            Destroy(gameObject);
        }
    }
}
