using UnityEngine;

namespace GenericSpaceSim.Core
{
    /// <summary>
    /// Responsible for movement and rotation of player ship.
    /// </summary>
    public class ShipMotor : IShipMotor
    {
        private readonly ShipInput shipInput;
        private readonly ShipCollisions shipCollisions;
        private readonly Transform targetTransform;
        private readonly MovementSettings movementSettings;
        private readonly RotationSettings rotationSettings;

        private Vector3 targetPos;
        private Vector3 smoothPos;

        public float CurrentSpeed { get; private set; } = 0f;
        public float CurrentRollSpeed { get; private set; } = 0f;

        public ShipMotor(
            MovementSettings movementSettings, RotationSettings rotationSettings, ShipInput playerInput, Transform target, ShipCollisions shipCollisions)
        {
            this.shipInput = playerInput;
            this.shipCollisions = shipCollisions;
            this.targetTransform = target;
            this.movementSettings = movementSettings;
            this.rotationSettings = rotationSettings;

            shipCollisions.OnCollisionOccured += ChangeSpeed;
        }

        ~ShipMotor() => shipCollisions.OnCollisionOccured -= ChangeSpeed;

        public void HandleMovement()
        {
            targetPos = targetTransform.position + targetTransform.forward * CurrentSpeed * Time.deltaTime;
            smoothPos = Vector3.Lerp(a: targetTransform.position, b: targetPos, t: 0.99f);

            // Moving ship by applying changes directly to its transform every frame. 
            targetTransform.position = smoothPos;

            // Smoothly changing the apply value over time on key presses.
            if (shipInput.WIsPressed)
                CurrentSpeed += movementSettings.DeltaMovementSpeed * Time.deltaTime;
            else if (shipInput.SIsPressed)
                CurrentSpeed -= movementSettings.DeltaMovementSpeed * movementSettings.BrakeForce * Time.deltaTime;

            // Mimic inertia force by passive speed decrease.
            else if (CurrentSpeed > 0.0001f)
                CurrentSpeed -= movementSettings.DeltaMovementSpeed * movementSettings.Inertia * Time.deltaTime;
            else if (CurrentSpeed < 0f)
                CurrentSpeed += movementSettings.DeltaMovementSpeed * movementSettings.Inertia * Time.deltaTime;

            // Setting speed limits.
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, -movementSettings.MaxSpeed / 6.0f, movementSettings.MaxSpeed);
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

        public void ExposeSpeedInfo(ref float currentSpeed, ref float currentRollSpeed)
        {
            currentSpeed = this.CurrentSpeed;
            currentRollSpeed = this.CurrentRollSpeed;
        }

        // Since we're using custom movement type, we gotta let external forces modify our speed somehow.
        private void ChangeSpeed(float delta) => CurrentSpeed
            = Mathf.Lerp(CurrentSpeed, CurrentSpeed / delta, 0.5f);
    }
}
