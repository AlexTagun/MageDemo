using System;

public class HealthFactory
{
    public IHealth Create(IHealthConfig config) =>
        config switch
        {
            HealthConfig c => new Health(c),
            HealthWithDefenceConfig c => new HealthWithDefence(c),
            _ => throw new ArgumentOutOfRangeException(nameof(config), config, "wrong health config"),
        };
}