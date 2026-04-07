using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] float DeathWaitTime;

    void Start()
    {
        PlayerDeath.OnDeath += HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        PlayerDeath.OnDeath -= HandlePlayerDeath;

        StartCoroutine(ResetSceneCoroutine(DeathWaitTime));
    }

    private IEnumerator ResetSceneCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(Settings.Instance.MainSceneName);
    }

    void OnDestroy()
    {
        PlayerDeath.OnDeath -= HandlePlayerDeath;
    }
}
