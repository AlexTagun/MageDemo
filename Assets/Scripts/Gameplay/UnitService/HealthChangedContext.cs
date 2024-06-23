public readonly struct HealthChangedContext
{
    public readonly IUnitView Source;
    public readonly IUnitView Receiver;
    public readonly float Delta;
    public readonly float NewHealthPercentage;

    public HealthChangedContext(DamageContext ctx, float newHealthPercentage)
    {
        Source = ctx.Source;
        Receiver = ctx.Receiver;
        Delta = -ctx.Damage;
        NewHealthPercentage = newHealthPercentage;
    }

    public HealthChangedContext(HealContext ctx, float newHealthPercentage)
    {
        Source = ctx.Source;
        Receiver = ctx.Receiver;
        Delta = ctx.Value;
        NewHealthPercentage = newHealthPercentage;
    }
}