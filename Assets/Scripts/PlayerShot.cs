using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private Vector2 _direction;
    Vector2 _velocity;

    public void Init(Vector2 playerVelocity, Vector2 direction)
    {
        _direction = direction.normalized;
        _velocity = playerVelocity + _direction * Settings.Instance.PlayerShotSpeed;
    }

    void Update()
    {
        Vector3 deltaPos = _velocity * Time.deltaTime;
        transform.position += deltaPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyChase>() != null)
        {
            Destroy(gameObject);
        }
    }
}
