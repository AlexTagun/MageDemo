using UnityEngine;

public class DebugDeath : IDeath
{
    private readonly string _id;

    public DebugDeath(string id)
    {
        _id = id;
    }

    public void OnDeath(IUnitView source)
    {
        Debug.Log($"[Unit][Death] id = {_id}");
    }
}