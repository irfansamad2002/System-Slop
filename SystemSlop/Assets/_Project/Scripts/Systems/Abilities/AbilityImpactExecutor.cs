using Project.Systems.Abilities.Data;
using Project.Systems.Ability.Data;
using System.Collections.Generic;
using UnityEngine;

public class AbilityImpactExecutor
{
    public void ExecuteTarget(GameObject target, AbilityData ability, ImpactData impactData)
    {
        if (target == null)
            return;

        ExecuteTarget(target, ability, impactData,1f);

        
    }

    public void ExecuteTarget(GameObject target, AbilityData ability, ImpactData impactData, float multiplier)
    {
        foreach (var effect in ability.effects)
        {
            effect.Apply(target, impactData, multiplier);
        }
    }


    public void ExecuteTargets(IEnumerable<GameObject> targets, AbilityData ability, ImpactData impactData)
    {
        foreach (var target in targets)
        {
            ExecuteTarget(target, ability, impactData);
        }
    }


    public void ExecuteTargets(IEnumerable<GameObject> targets, AbilityData ability, ImpactData impactData, ProjectileData projectileData)
    {
        foreach (var target in targets)
        {
            float falloff = CalculateFalloff(target,ability,impactData,projectileData);

            ExecuteTarget(target, ability, impactData, falloff);
        }
    }

    private float CalculateFalloff(GameObject target, AbilityData ability, ImpactData impactData, ProjectileData projectileData)
    {
        float distance = Vector3.Distance(impactData.impactPoint, target.GetComponent<Collider>().ClosestPoint(impactData.impactPoint));
        if (distance <= projectileData.minDistanceThreshold)
        {
            distance = 0f; // treat as direct hit
        }
        float normalized = distance / ability.impactSettings.radius;
        normalized = Mathf.Clamp01(normalized);

        float falloff = Mathf.Pow(1f - normalized, .5f); // quadratic falloff
        falloff = Mathf.Max(falloff, projectileData.minFalloff); // ensure minimum effect

        return falloff;
    }
}
