using UnityEngine;

public class EnemyView : MonoBehaviour, IUnitView
{
    [SerializeField] private Rigidbody _rigidbody;

    private Transform _transform;

    public void Init()
    {
        _transform = transform;
    }

    public Vector3 GetPosition() => _transform.position;

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }
}