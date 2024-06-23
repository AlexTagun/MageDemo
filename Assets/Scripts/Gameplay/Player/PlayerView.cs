using System;
using UnityEngine;

public class PlayerView : MonoBehaviour, IUnitView
{
    public event Action<Collision> CollisionStayed;

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
    
    public void SetAngularVelocity(Vector3 velocity)
    {
        _rigidbody.angularVelocity = velocity;
    }

    private void OnCollisionStay(Collision other)
    {
        CollisionStayed?.Invoke(other);
    }
}