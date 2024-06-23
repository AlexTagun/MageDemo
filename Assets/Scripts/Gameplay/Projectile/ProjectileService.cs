using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectileService : IUpdate
{
    [Inject] private UnitService _unitService;

    private class Context
    {
        public readonly float Damage;
        public readonly UnitRole RoleForTargets;
        public readonly IUnitView SourceView;

        public Context(CreateProjectileRequest request)
        {
            Damage = request.Damage;
            RoleForTargets = request.RoleForTargets;
            SourceView = request.SourceView;
        }
    }

    private readonly Dictionary<ProjectileView, Context> _contexts = new();
    private readonly List<ProjectileView> _projectilesToDestroy = new();

    void IUpdate.Update()
    {
        foreach (var view in _projectilesToDestroy)
        {
            _contexts.Remove(view);
            Object.Destroy(view.gameObject);
        }

        _projectilesToDestroy.Clear();
    }

    public void Create(CreateProjectileRequest request)
    {
        var view = Object.Instantiate(request.ViewPrefab, request.Origin, Quaternion.LookRotation(request.Direction));
        _contexts.Add(view, new Context(request));
        view.SetVelocity(request.Speed * request.Direction);

        view.TriggerEntered += other =>
        {
            var unitView = other.GetComponent<IUnitView>();

            if (unitView == null)
            {
                _projectilesToDestroy.Add(view);
                return;
            }

            OnTriggerEntered(unitView, view);
        };
    }

    private void OnTriggerEntered(IUnitView unitView, ProjectileView view)
    {
        var context = _contexts[view];

        if (_unitService.IsExist(unitView) == false || context.RoleForTargets != _unitService.GetRole(unitView))
        {
            return;
        }

        var damageContext = new DamageContext(context.SourceView, unitView, context.Damage);
        _unitService.TakeDamage(damageContext);
        _projectilesToDestroy.Add(view);
    }
}