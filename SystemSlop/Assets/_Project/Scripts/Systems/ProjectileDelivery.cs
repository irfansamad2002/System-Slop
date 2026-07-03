using Project.Systems.Abilities.Data;
using Project.Systems.Combat;
using UnityEngine;

public class ProjectileDelivery
{
    private readonly AbilityImpactExecutor _executor;

    public ProjectileDelivery(AbilityImpactExecutor executor)
    {
        _executor = executor;
    }

    public void Execute(Transform firePoint, Vector3 destination, AbilityData ability)
    {

        Vector3 dir = (destination - firePoint.position).normalized;

        var projectileGO = Object.Instantiate(ability.deliverySettings.projectile.prefab,
            firePoint.position,
            Quaternion.LookRotation(dir));

        var projectile = projectileGO.GetComponent<Projectile>();

        projectile.Init(_executor, ability);



        Object.Destroy(projectileGO, ability.deliverySettings.projectile.lifetime);
    }

    
}
