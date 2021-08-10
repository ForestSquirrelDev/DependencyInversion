using UnityEngine;

namespace GenericSpaceSim.Ship
{
    /// <summary>
    /// Responsible for movement and rotation of player ship.
    /// </summary>
    public class ShipMotor
    {
        private readonly ShipInput shipInput;
        private readonly ShipCollisions shipCollisions;
        private readonly Transform targetTransform;
        private readonly MovementSettings movementSettings;

        private Vector3 targetPos;
        private Vector3 smoothPos;

        public float CurrentSpeed { get; private set; } = 0f;

        public ShipMotor(
            MovementSettings movementSettings, ShipInput playerInput, Transform target, ShipCollisions shipCollisions)
        {
            this.shipInput = playerInput;
            this.shipCollisions = shipCollisions;
            this.targetTransform = target;
            this.movementSettings = movementSettings;

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

        public void ExposeSpeedInfo(ref float currentSpeed) => currentSpeed = this.CurrentSpeed;

        // Since we're using custom movement type, we gotta let external forces modify our speed somehow.
        private void ChangeSpeed(float delta) => CurrentSpeed
            = Mathf.Lerp(CurrentSpeed, CurrentSpeed / delta, 0.5f);
    }
}
