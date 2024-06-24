using UnityEngine;

public class DebugDeathHandler : IDeathHandler
{
    private readonly string _id;

    public DebugDeathHandler(string id)
    {
        _id = id;
    }

    public void OnDeath(IUnitView source)
    {
        Debug.Log($"[Unit][Death] id = {_id}");
    }
}