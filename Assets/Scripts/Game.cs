using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] float DeathWaitTime;

    void Start()
    {
        Resume();
        PlayerDeath.OnDeath += HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        PlayerDeath.OnDeath -= HandlePlayerDeath;
        Pause();
        StartCoroutine(ResetSceneCoroutine(DeathWaitTime));
    }

    private IEnumerator ResetSceneCoroutine(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(Settings.Instance.MainSceneName);
    }

    void OnDestroy()
    {
        PlayerDeath.OnDeath -= HandlePlayerDeath;
    }

    private void Pause()
    {
        Time.timeScale = 0;
    }

    private void Resume()
    {
        Time.timeScale = 1;
    }
}
