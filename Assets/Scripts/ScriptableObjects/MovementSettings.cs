using UnityEngine;

namespace GenericSpaceSim
{
    [CreateAssetMenu(menuName = "Movement settings", fileName = "ShipMovementData")]
    public class MovementSettings : ScriptableObject
    {
        [Range(1.0f, 400.0f)]
        [Tooltip("Maximum speed of player ship.")]
        [SerializeField] private float maxSpeed = 200.0f;

        [Range(1.0f, 200.0f)]
        [Tooltip("How fast the ship's position is changed through time.\nThe less this value is, the smoother (or heavier) movement feels.")]
        [SerializeField] private float deltaMovementSpeed = 25.0f;

        [Range(1.0f, 2.0f)]
        [Tooltip("How fast the ship should lose its speed when using brake (S button).\nValue of 1 means it's the same as acceleration.")]
        [SerializeField] private float brakeForce = 1.5f;

        [Range(0.01f, 1.0f)]
        [Tooltip("How fast the ship will be losing its speed over time when not accelerating.\nBigger value -> ship will lose speed faster.")]
        [SerializeField] private float inertia = 0.1f;

        public float MaxSpeed => maxSpeed;
        public float DeltaMovementSpeed => deltaMovementSpeed;
        public float BrakeForce => brakeForce;
        public float Inertia => inertia;
    }
}
