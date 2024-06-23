using UnityEngine;

public class Health : IHealth
{
    private readonly float _max;

    private float _current;

    public Health(float max)
    {
        _max = max;
        _current = max;
    }

    public Health(HealthConfig config) : this(config.MaxHealth)
    {
    }

    public float GetCurrent() => _current;

    public float GetMax() => _max;

    public void ReceiveHit(float damage)
    {
        _current -= damage;
        ClampCurrent();
    }

    public void ReceiveHeal(float heal)
    {
        _current += heal;
        ClampCurrent();
    }

    private void ClampCurrent()
    {
        _current = Mathf.Clamp(_current, 0, _max);
    }
}