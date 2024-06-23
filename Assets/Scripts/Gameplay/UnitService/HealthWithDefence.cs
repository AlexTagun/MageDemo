public class HealthWithDefence : IHealth
{
    private readonly Health _health;
    private readonly float _defence;

    public HealthWithDefence(HealthWithDefenceConfig config)
    {
        _health = new Health(config.MaxHealth);
        _defence = config.Defence;
    }

    public float GetCurrent() => _health.GetCurrent();

    public float GetMax() => _health.GetMax();

    public void ReceiveHit(float damage) => _health.ReceiveHit(_defence * damage);

    public void ReceiveHeal(float heal) => _health.ReceiveHeal(heal);
}