using UnityEngine;

namespace GenericSpaceSim.Ship
{
    /// <summary>
    /// Rotation and tilting of the ship.
    /// </summary>
    public class ShipRotator
    {
        private readonly ShipInput shipInput;
        private readonly Transform targetTransform;
        private readonly RotationSettings rotationSettings;

        public float CurrentRollSpeed { get; private set; } = 0f;

        public ShipRotator(RotationSettings rotationSettings, ShipInput shipInput, Transform target)
        {
            this.rotationSettings = rotationSettings;
            this.shipInput = shipInput;
            this.targetTransform = target;
        }

        public void HandleRotation()
        {
            // Rotate ship's Transform by X and Y axes according to mouse position input.
            targetTransform.Rotate(xAngle: shipInput.Pitch * rotationSettings.RotationSpeed * Time.deltaTime,
                                   yAngle: shipInput.Yaw * rotationSettings.RotationSpeed * Time.deltaTime,
                                   zAngle: 0f);
        }

        public void HandleTilting()
        {
            if (CurrentRollSpeed > 0f || CurrentRollSpeed < 0f)
                targetTransform.Rotate(0f, 0f, CurrentRollSpeed * Time.deltaTime);

            if (shipInput.QIsPressed)
                CurrentRollSpeed += Time.deltaTime * rotationSettings.DeltaRollSpeed;
            else if (shipInput.EIsPressed)
                CurrentRollSpeed -= Time.deltaTime * rotationSettings.DeltaRollSpeed;

            else if (CurrentRollSpeed > 0f)
                CurrentRollSpeed = Mathf.Lerp(a: CurrentRollSpeed,
                                            b: CurrentRollSpeed - Time.deltaTime
                                            * rotationSettings.DeltaRollSpeed
                                            * rotationSettings.Inertia,
                                            t: rotationSettings.RollLerpTime);
            else if (CurrentRollSpeed < 0f)
                CurrentRollSpeed = Mathf.Lerp(a: CurrentRollSpeed,
                                           b: CurrentRollSpeed + Time.deltaTime
                                           * rotationSettings.DeltaRollSpeed
                                           * rotationSettings.Inertia,
                                           t: rotationSettings.RollLerpTime);

            CurrentRollSpeed = Mathf.Clamp(CurrentRollSpeed, -rotationSettings.MaxRollSpeed, rotationSettings.MaxRollSpeed);
        }
    }
}
