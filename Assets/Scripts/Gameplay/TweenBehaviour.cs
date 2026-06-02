using System;
using UnityEngine;

public class TweenBehaviour : MonoBehaviour
{
    [SerializeField] private float _time = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Move example
        LeanTween.move(gameObject, to: new Vector3(5, 0, 0), time: _time).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, to: new Vector3(2, 2, 2), time: _time);
        });

        float someNumber = 0;

        LeanTween.value(gameObject: gameObject, from: someNumber, to: 100, _time).setOnUpdate(UpdateValue);

        // Scale example
        //LeanTween.scale(gameObject, to: new Vector3(2, 2, 2), time: _time);

        // Rotate example
        //LeanTween.rotate(gameObject, to: new Vector3(0, 0, 180), time: _time);
    }


    private void UpdateValue(float value)
    {
        Debug.Log(value);   
    }

    /*
    private void HandleTweenEnd()
    {
        LeanTween.scale(gameObject, to: new Vector3(2, 2, 2), time: _time);
    }
    */

}
