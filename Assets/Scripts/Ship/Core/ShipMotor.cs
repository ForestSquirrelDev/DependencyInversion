using UnityEngine;
using GenericSpaceSim.Variables;

namespace GenericSpaceSim.Ship
{
    /// <summary>
    /// Responsible for movement of player ship.
    /// </summary>
    public class ShipMotor
    {
        private readonly ShipInput shipInput;
        private readonly ShipCollisions shipCollisions;
        private readonly Transform targetTransform;
        private readonly MovementSettings movementSettings;
        private readonly FloatVariable currentSpeed;

        private Vector3 targetPos;
        private Vector3 smoothPos;

        public ShipMotor(
            MovementSettings moveSettings, FloatVariable shipSpeed, ShipInput playerInput, Transform target, ShipCollisions shipCollisions)
        {
            this.shipInput = playerInput;
            this.shipCollisions = shipCollisions;
            this.targetTransform = target;
            this.movementSettings = moveSettings;
            this.currentSpeed = shipSpeed;

            shipCollisions.OnCollisionOccured += ChangeSpeed;
        }

        ~ShipMotor() => shipCollisions.OnCollisionOccured -= ChangeSpeed;

        public void HandleMovement()
        {
            targetPos = targetTransform.position
                        + targetTransform.forward
                        * currentSpeed.Value
                        * Time.deltaTime;

            smoothPos = Vector3.Lerp(a: targetTransform.position,
                                     b: targetPos,
                                     t: 0.99f);

            // Moving ship by applying changes directly to its transform every frame. 
            targetTransform.position = smoothPos;

            // Smoothly changing the apply value over time on key presses.
            if (shipInput.WIsPressed)
                currentSpeed.Value += movementSettings.DeltaMovementSpeed
                                      * Time.deltaTime;
            else if (shipInput.SIsPressed)
                currentSpeed.Value -= movementSettings.DeltaMovementSpeed
                                      * movementSettings.BrakeForce
                                      * Time.deltaTime;

            // Mimic inertia force by passive speed decrease.
            else if (currentSpeed.Value > 0.0001f)
                currentSpeed.Value -= movementSettings.DeltaMovementSpeed
                                      * movementSettings.Inertia
                                      * Time.deltaTime;
            else if (currentSpeed.Value < 0f)
                currentSpeed.Value += movementSettings.DeltaMovementSpeed
                                      * movementSettings.Inertia
                                      * Time.deltaTime;

            // Setting speed limits.
            currentSpeed.Value = Mathf.Clamp(value: currentSpeed.Value,
                                             min: -movementSettings.MaxSpeed / 6.0f,
                                             max: movementSettings.MaxSpeed);
        }

        // Since we're using custom movement type, we gotta let external forces modify our speed somehow.
        private void ChangeSpeed(float delta) => currentSpeed.Value
            = Mathf.Lerp(currentSpeed.Value, currentSpeed.Value / delta, 0.6f);
    }
}
