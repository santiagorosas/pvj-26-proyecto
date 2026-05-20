using UnityEngine;

public class CurveBehaviour : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;

    [SerializeField] private float _speed;
    [SerializeField] private float _amplitude;


    private float _time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if (_time > 1)
        {
            _time = 0;
        }
        float curveValue = _curve.Evaluate(_time * _speed) * _amplitude;

        Vector2 pos = transform.position;
        pos.y = curveValue;
        transform.position = pos;        
    }
}
