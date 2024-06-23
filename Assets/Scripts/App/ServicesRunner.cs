using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ServicesRunner : MonoBehaviour
{
    private List<IStart> _start;
    private List<IUpdate> _update;

    [Inject]
    public void Init(DiContainer container)
    {
        Debug.Log("ServicesRunner.Init");
        _start = container.ResolveAll<IStart>();
        _update = container.ResolveAll<IUpdate>();
    }

    private void Start()
    {
        Debug.Log("ServicesRunner.Start");
        foreach (var start in _start)
        {
            start.Start();
        }
    }

    private void Update()
    {
        foreach (var update in _update)
        {
            update.Update();
        }
    }
}