using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Transform _playerTransform;

    void Start()
    {
        _playerTransform = GameObject.FindAnyObjectByType<PlayerMovement>().transform;

        if (_playerTransform == null)
        {
            throw new UnityException();
        }
    }

    void Update()
    {
        Vector2 playerPosition = _playerTransform.position;
        Vector2 myPosition = transform.position;

        // La dirección del punto A al punto B es el vector B-A
        Vector2 direction = playerPosition - myPosition;

        Vector3 velocity = direction.normalized * Settings.Instance.EnemySpeed;
        transform.position += velocity * Time.deltaTime;
    }
}
