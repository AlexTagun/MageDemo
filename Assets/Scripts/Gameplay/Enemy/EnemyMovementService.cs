using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMovementService : IStart, IUpdate
{
    private class Context
    {
        public readonly float MovementSpeed;

        public Context(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
        }
    }

    [Inject] private PlayerService _playerService;

    private readonly Dictionary<EnemyView, Context> _contexts = new();
    private PlayerView _playerView;

    void IStart.Start()
    {
        _playerView = _playerService.PlayerView;
    }

    void IUpdate.Update()
    {
        foreach (var (view, context) in _contexts)
        {
            var direction = (_playerView.GetPosition() - view.GetPosition()).normalized;
            var velocity = context.MovementSpeed * direction;
            view.SetVelocity(velocity);
            view.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }

    public void Add(EnemyView view, float movementSpeed) => _contexts.Add(view, new Context(movementSpeed));

    public void Remove(EnemyView view) => _contexts.Remove(view);
}