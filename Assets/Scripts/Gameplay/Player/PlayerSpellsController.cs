using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerSpellsController : IStart, IUpdate
{
    private const UnitRole TargetsRole = UnitRole.Enemy;
    
    private readonly LinkedList<ISpell> _spells = new();
    private readonly PlayerViewProvider _playerViewProvider;
    private readonly SpellFactory _spellFactory;
    private readonly PlayerSettings _playerSettings;

    private LinkedListNode<ISpell> _selectedSpell;

    [Inject]
    public PlayerSpellsController(PlayerViewProvider playerViewProvider, SpellFactory spellFactory, GameplaySettings gameplaySettings)
    {
        _playerViewProvider = playerViewProvider;
        _spellFactory = spellFactory;
        _playerSettings = gameplaySettings.PlayerSettings;
    }

    void IStart.Start()
    {
        foreach (var spellConfig in _playerSettings.SpellConfigs)
        {
            var spell = _spellFactory.Create(spellConfig, _playerViewProvider.GetView(), TargetsRole);
            _spells.AddLast(spell);
        }

        _selectedSpell = _spells.First;
    }

    void IUpdate.Update()
    {
        if (Input.GetKeyDown(_playerSettings.CastSpellKey))
        {
            _selectedSpell.Value.Cast();
        }
        
        if (Input.GetKeyDown(_playerSettings.NextSpellKey))
        {
            _selectedSpell = _selectedSpell.Next ?? _spells.First;
        }
        
        if (Input.GetKeyDown(_playerSettings.PreviousSpellKey))
        {
            _selectedSpell = _selectedSpell.Previous ?? _spells.Last;
        }
    }
}