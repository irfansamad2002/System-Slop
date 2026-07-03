using UnityEngine;

public struct AbilityTargetingData
{
    // ==== CAST / INTENT ====

    // Optional cast-time selected target
    public GameObject target;

    // Raw aimed point from targeting
    public Vector3 targetPoint;

    // Shared directinal intent
    public Vector3 direction;

    // Whether target point exists
    public bool hasTargetPoint;
}