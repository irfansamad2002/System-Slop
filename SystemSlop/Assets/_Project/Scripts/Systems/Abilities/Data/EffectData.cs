using UnityEngine;

namespace Project.Systems.Effects
{
    public abstract class EffectData : ScriptableObject
    {
        public string effectId; // So that dont apply effects, u apply them repeatdly over time

        public virtual void Apply(
         GameObject target,
         ImpactData impactData = default,
         float multiplier = 1f)
        {
            Apply(target, multiplier);
        }

        public virtual void Apply(
            GameObject target,
            float multiplier = 1f)
        {
            // fallback implementation
        }
    }
}