using UnityEngine;

public class DebugHealthChangedHandler : IHealthChangedHandler
{
    private readonly string _id;

    public DebugHealthChangedHandler(string id)
    {
        _id = id;
    }

    public void OnHealthChanged(HealthChangedContext ctx)
    {
        Debug.Log($"[Unit][HealthChanged] id = {_id}, percentage = {ctx.NewHealthPercentage}");
    }
}