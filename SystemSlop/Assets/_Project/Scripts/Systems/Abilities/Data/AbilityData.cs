using Project.Systems.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.Abilities.Data
{
    [CreateAssetMenu(menuName = "Abilities/Ability")]
    public class AbilityData : ScriptableObject
    {
        public string abilityName;
        public string description;
        public Sprite icon;

        public float cooldown;

        public TargetingSettings targetingSettings;
        public DeliverySettings deliverySettings;
        public ImpactSettings impactSettings;

        public List<EffectData> effects;

    }
}

public enum TargetingType
{
    Point, //   picka position in world
    Target,//   pick an entity
    Self//      always yourself
}

public enum DeliveryType
{
    Instant,
    Projectile,
    Delayed
}

public enum CastMode
{
    Instant,    //Cast immediately on press
    Confirm     //enter targeting mode first
}

public enum AreaShape
{
    None,
    Sphere,
    Cone
}