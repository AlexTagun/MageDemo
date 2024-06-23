using UnityEngine;

public class DebugHealthChanged : IHealthChanged
{
    private readonly string _id;

    public DebugHealthChanged(string id)
    {
        _id = id;
    }

    public void OnHealthChanged(HealthChangedContext ctx)
    {
        Debug.Log($"[Unit][HealthChanged] id = {_id}, percentage = {ctx.NewHealthPercentage}");
    }
}