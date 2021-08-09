using UnityEngine;

namespace GenericSpaceSim
{
    [CreateAssetMenu(menuName = "Rotation settings", fileName = "ShipRotationData")]
    public class RotationSettings : ScriptableObject
    {
        [Range(1.0f, 300.0f)]
        [Tooltip("How fast the ship is reacting to mouse position.")]
        [SerializeField] private float rotationSpeed = 120.0f;

        [Range(1.0f, 200.0f)]
        [Tooltip("How fast the ship turns by Z axis (i.e. rolls).")]
        [SerializeField] private float deltaRollSpeed = 100.0f;

        [Range(1.0f, 200.0f)]
        [Tooltip("Current roll speed will be clamped between this value and its negative representation.")]
        [SerializeField] private float maxRollSpeed = 100.0f;

        [Range(1.0f, 10.0f)]
        [Tooltip("Power of inertia for roll. The bigger this value is, the faster ship stops rolling after key is released.")]
        [SerializeField] private float inertia = 1.5f;

        [Range(0.01f, 1.0f)]
        [Tooltip("Amount of smoothing for roll. Smaller value -> more smoothed motion.")]
        [SerializeField] private float rollLerpTime = 0.4f;

        public float RotationSpeed => rotationSpeed;
        public float DeltaRollSpeed => deltaRollSpeed;
        public float MaxRollSpeed => maxRollSpeed;
        public float Inertia => inertia;
        public float RollLerpTime => rollLerpTime;
    }
}
