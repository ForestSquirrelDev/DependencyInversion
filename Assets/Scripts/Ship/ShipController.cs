using UnityEngine;

namespace GenericSpaceSim.Core
{
    /// <summary>
    /// Creates a set of core systems instances to control the ship.
    /// </summary>
    public class ShipController : MonoBehaviour
    {
        [SerializeField] private MovementSettings movementSettings;
        [SerializeField] private RotationSettings rotationSettings;

        [SerializeField] private Transform targetShip;

        private IShipMotor shipMotor;
        private ShipInput shipInput;
        private ShipCollisions shipCollisions;

        private float currentSpeed;
        private float currentRollSpeed;

        public float CurrentSpeed => currentSpeed;
        public float CurrentRollSpeed => currentRollSpeed;

        private void Awake()
        {
            if (targetShip == null)
                targetShip = this.transform;

            shipCollisions = gameObject.AddComponent(typeof(ShipCollisions)) as ShipCollisions;

            shipInput = new ShipInput();
            shipMotor = new ShipMotor(movementSettings, rotationSettings, shipInput, targetShip, shipCollisions);
        }

        private void Update()
        {
            shipInput.HandleKeyboardInput();
            shipInput.HandleControllerInput();

            shipMotor.HandleMovement();
            shipMotor.HandleRotation();
            shipMotor.HandleTilting();
            shipMotor.ExposeSpeedInfo(ref currentSpeed, ref currentRollSpeed);
        }
    }
}
