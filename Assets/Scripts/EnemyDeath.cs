using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    public static event UnityAction OnDeath;

    private EnemyManager _enemyManager;

    public void Init(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerShot>())
        {
            SoundManager.Instance.EnemyDeath.Play();
            Destroy(gameObject);
            _enemyManager.CreateEnemyBlood(transform.position);
            OnDeath?.Invoke();
        }
    }
}
