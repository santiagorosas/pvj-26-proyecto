using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private float _createEnemyInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        if (_enemyPrefab == null)
        {
            throw new UnityException("Couldn't load Enemy prefab from Resources!");
        }

        for (int i = 0; i < Settings.Instance.StartingEnemyCount; i++)
        {
            CreateEnemy();
        }

        _createEnemyInterval = Settings.Instance.BaseCreateEnemyInterval;
        StartCoroutine(CreateEnemiesCoroutine());
    }

    private IEnumerator CreateEnemiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_createEnemyInterval);
            CreateEnemy();
        }
    }


    private void CreateEnemy()
    {
        GameObject newEnemy = Instantiate(_enemyPrefab);
        newEnemy.transform.position = new Vector2(
            x: Random.Range(-8, 8),
            y: Random.Range(-4, 4));
    }

    void Update()
    {
        _createEnemyInterval -= Time.deltaTime * Settings.Instance.CreateEnemyIntervalDecreasePerSecond;

        if (_createEnemyInterval < Settings.Instance.MinCreateEnemyInterval)
        {
            _createEnemyInterval = Settings.Instance.MinCreateEnemyInterval;
        }
    }
}
