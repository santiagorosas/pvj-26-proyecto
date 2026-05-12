using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystemPrefab;
    [SerializeField] private int _minParticleCount;
    [SerializeField] private int _maxParticleCount;

    public void CreateExplosion(Vector3 position)
    {
        ParticleSystem system = Instantiate(_particleSystemPrefab, position: position, rotation: Quaternion.identity);

        var emission = system.emission;
        var burst = emission.GetBurst(0);
        burst.count = Random.Range(_minParticleCount, _maxParticleCount);
        emission.SetBurst(0, burst);

        system.Play();
    }
}
