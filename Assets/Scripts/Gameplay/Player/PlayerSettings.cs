using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    public float MovementSpeed;

    [SerializeReference] public IHealthConfig HealthConfig;

    public KeyCode CastSpellKey;
    public KeyCode PreviousSpellKey;
    public KeyCode NextSpellKey;

    [SerializeReference] public List<ISpellConfig> SpellConfigs;
}