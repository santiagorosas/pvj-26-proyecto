using UnityEngine;

public class DepthSorter : MonoBehaviour
{
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        if (_renderer == null) throw new UnityException("DepthSorter: No rendered in children!");
    }

    void Update()
    {
        _renderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }
}
