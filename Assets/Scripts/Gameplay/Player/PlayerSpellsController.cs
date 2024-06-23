using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellsController
{
    private readonly LinkedList<ISpell> _spells = new();
    private readonly SpellFactory _spellFactory;
    private readonly PlayerSettings _playerSettings;

    private LinkedListNode<ISpell> _selectedSpell;

    public PlayerSpellsController(SpellFactory spellFactory, PlayerSettings playerSettings)
    {
        _spellFactory = spellFactory;
        _playerSettings = playerSettings;
    }

    public void Init()
    {
        foreach (var spellConfig in _playerSettings.SpellConfigs)
        {
            var spell = _spellFactory.Create(spellConfig);
            _spells.AddLast(spell);
        }

        _selectedSpell = _spells.First;
    }

    public void Update()
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