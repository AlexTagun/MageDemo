using UnityEngine;

public class EnemyView : MonoBehaviour, IUnitView
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _healthBarOffsetY;

    private Transform _transform;

    public void Init()
    {
        _transform = transform;
    }

    public Vector3 GetPosition() => _transform.position;

    public float GetHealthBarOffsetY() => _healthBarOffsetY;

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }
}