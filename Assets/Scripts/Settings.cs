using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    
    static public Settings Instance;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        Debug.Log("init");
        Instance = Resources.Load<Settings>("Settings");
    }

    [Tooltip("Defines the speed of the player")]
    public float PlayerSpeed;
    public int CoinCount;
    public int StartingEnemyCount;
    public float PlayerForce = 10;
    public float EnemySpeed = 8;
    public float PlayerShootInterval = 1.5f;
    public float PlayerShotSpeed = 12;
    public float BaseCreateEnemyInterval = 3;
    public float CreateEnemyIntervalDecreasePerSecond = 0.1f;
    public float MinCreateEnemyInterval = 0.3f;
    public string MainSceneName = "Main";
    public int ScorePerEnemy = 5;
}
