using Project.Systems.Abilities.Data;
using Project.Systems.Combat.Query;
using UnityEngine;

namespace Project.Systems.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Material transparentMaterial;
        private float _speed;
        private float _explosionRadius;
        private LayerMask _damageLayers;
        private GameObject _impactVFX;

        private bool _hasHit;
        private AbilityImpactExecutor _impactExecutor;
        private AbilityData _ability;


        public void Init(AbilityImpactExecutor impactExecutor , AbilityData ability)
        {
            _impactExecutor = impactExecutor;
            _ability = ability;

            _speed = _ability.deliverySettings.projectile.speed;
            _explosionRadius = ability.impactSettings.radius;
            _damageLayers = ability.impactSettings.targetLayers;
            _impactVFX = ability.deliverySettings.impactVFX;
        }

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_hasHit) return; // prevent multiple hits
            _hasHit = true;

            //Debug.Log(other.name);
            var impactData = new ImpactData()
            {
                direction = transform.forward,
                impactPoint = transform.position
            };
            Explode(impactData);

            SpawnDebugSphere(transform.position, _explosionRadius);
    
            Destroy(gameObject);

        }
        private void Explode(ImpactData impactData)
        {

            Vector3 explosionCenter = transform.position;

            SpawnImpactVFX(explosionCenter);

            var targets = AreaQuery.GetTargetsSphere(explosionCenter, _explosionRadius, _damageLayers);

            _impactExecutor.ExecuteTargets(targets, _ability, impactData, _ability.deliverySettings.projectile);

        }

        private void SpawnDebugSphere(Vector3 position, float radius)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            sphere.transform.position = position;
            sphere.transform.localScale = Vector3.one * radius * 2f; // scale to match explosion radius
            //Debug.Log("Debug Sphere localscale " + radius);

            // remove collider so it doesn't interfere
            Destroy(sphere.GetComponent<Collider>());

            // optional: make it semi-transparent
            var renderer = sphere.GetComponent<Renderer>();
            renderer.material = transparentMaterial;

            Destroy(sphere, 1f); // auto cleanup
        }

        private void SpawnImpactVFX(Vector3 position)
        {
            //Debug.Log($"Impact VFX :{_impactVFX}");
            if (_impactVFX == null) return;

            var vfx = Instantiate(_impactVFX, position, Quaternion.identity);

            //scale to match explosion raidus
            float diameter = _explosionRadius * 2f;
            vfx.transform.localScale = Vector3.one * diameter;

            Destroy(vfx, 2f);// Cleanup
        }
    }
}