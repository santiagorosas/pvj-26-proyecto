using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    //private PlayerMovement _playerMovement;

    [SerializeField] private Transform _target;
    [SerializeField] private float _z;

    void Awake()
    {
        if (_target == null) throw new UnityException("CameraFollow: Target is null!");
    }
    
    void Start()
    {
        //_playerMovement = Utils.Find<PlayerMovement>();
        //_playerMovement = PlayerMovement.Instance;
    }

    void Update()
    {
        Vector3 targetPos = _target.position;
        targetPos.z = _z;
        transform.position = targetPos;
    }
}
