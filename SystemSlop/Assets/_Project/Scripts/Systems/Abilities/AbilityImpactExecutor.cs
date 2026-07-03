using Project.Systems.Abilities.Data;
using System.Collections.Generic;
using UnityEngine;

public class AbilityImpactExecutor
{
    public void ExecuteTarget(GameObject target, AbilityData ability, ImpactData impactData)
    {
        if (target == null)
            return;

        foreach (var effect in ability.effects)
        {
            effect.Apply(target, impactData);
        }
    }

    public void ExecuteTargets(IEnumerable<GameObject> targets, AbilityData ability, ImpactData impactData)
    {
        foreach (var target in targets)
        {
            ExecuteTarget(target, ability, impactData);
        }
    }
}
