using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private static HUD _instance;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _hpFillImage;

    private int _score;

    void Start()
    {
        if (_instance != null) // Ya existía un HUD así que me destruyo y no hago nada más
        {
            Destroy(gameObject);
            return;
        }

        //transform.Find("TestButton").GetComponent<Button>().onClick.AddListener(() => Debug.Log("click3"));
        EnemyDeath.OnDeath += HandleEnemyDeath;

        if (_scoreText == null)
        {
            _scoreText = transform.Find("Score").GetComponent<TextMeshProUGUI>();

            if (_scoreText == null)
            {
                throw new UnityException("Can't find Score under HUD!");
            }
        }

        DontDestroyOnLoad(gameObject);
        _instance = this;
    }

    private void HandleEnemyDeath()
    {
        _score += Settings.Instance.ScorePerEnemy;
        _scoreText.text = _score.ToString();
    }

    private void HandlePlayerHit()
    {
        _hpFillImage.fillAmount = 0.5f;
           
    }

    public void HandleTestButtonClick()
    {
        Debug.Log("click2");
    }

}
