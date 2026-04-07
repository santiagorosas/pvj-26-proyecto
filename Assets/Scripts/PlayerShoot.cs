using System;
using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameObject _shotPrefab;
    private PlayerMovement _movement;

    private Vector2 Direction
    {
        get
        {
            return _movement.LastNonZeroDirection;
        }
    }

    void Awake()
    {
        _shotPrefab = Resources.Load<GameObject>("Prefabs/PlayerShot");
        _movement = GetComponent<PlayerMovement>();

        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Settings.Instance.PlayerShootInterval);
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject shotObject = Instantiate(original: _shotPrefab, position: transform.position, rotation: Quaternion.identity);
        shotObject.GetComponent<PlayerShot>().Init(Direction);
    }


}