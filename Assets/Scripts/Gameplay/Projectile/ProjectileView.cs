using System;
using UnityEngine;

public class ProjectileView : MonoBehaviour
{
    public event Action<Collider> TriggerEntered;

    [SerializeField] private Rigidbody _rigidbody;

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEntered?.Invoke(other);
    }
}