public readonly struct DamageContext
{
    public readonly IUnitView Source;
    public readonly IUnitView Receiver;
    public readonly float Damage;

    public DamageContext(IUnitView source, IUnitView receiver, float damage)
    {
        Source = source;
        Receiver = receiver;
        Damage = damage;
    }
}