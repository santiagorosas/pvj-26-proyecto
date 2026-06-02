using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i=0; i<Settings.Instance.CoinCount; i++)
        {
            GameObject newCoin = Instantiate(_coinPrefab);   
                newCoin.transform.position = new Vector2(
            x: Random.Range(-8,8),
            y: Random.Range(-4,4));
        }
    }


}
