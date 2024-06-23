public readonly struct HealContext
{
    public readonly IUnitView Source;
    public readonly IUnitView Receiver;
    public readonly float Value;

    public HealContext(IUnitView source, IUnitView receiver, float value)
    {
        Source = source;
        Receiver = receiver;
        Value = value;
    }

    public HealContext(IUnitView receiver, float value)
    {
        Source = null;
        Receiver = receiver;
        Value = value;
    }
}