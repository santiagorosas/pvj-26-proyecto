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

        // When out of screen, destroy
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyChase>() != null)
        {
            Destroy(gameObject);
        }
    }
}
