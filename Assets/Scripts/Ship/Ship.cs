using UnityEngine;

namespace GenericSpaceSim.Core
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private MovementSettings movementSettings;
        [SerializeField] private RotationSettings rotationSettings;

        [SerializeField] private Transform targetShip;

        private ShipInput shipInput;
        private IShipMotor shipMotor;
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
