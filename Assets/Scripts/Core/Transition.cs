
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : ReusableBehaviour
{
    public static event UnityAction OnSceneLoaded;

    [SerializeField] private Image _blackScreen;
    [SerializeField] private float _totalDuration;

    private static Transition _instance;

    public static Transition Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Utils.Find<Transition>();
            }
            DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
    }

    public void ChangeScene(string sceneName)
    {
        /*
        LeanTween.value(gameObject, from: 0, to: 1, time: _totalDuration * 0.5f).setOnUpdate((float value) =>
        {
            Color color = _blackScreen.color;
            color.a = value;
            _blackScreen.color = color;
        });
        Esto es equivalente a lo siguiente
        */
        
        LeanTween.alphaCanvas(_blackScreen.GetComponent<CanvasGroup>(), to: 1, _totalDuration * 0.5f).setOnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += HandleSceneLoaded;
        });
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
        LeanTween.alphaCanvas(_blackScreen.GetComponent<CanvasGroup>(), to: 0, _totalDuration * 0.5f);
        OnSceneLoaded?.Invoke();
    }
}
