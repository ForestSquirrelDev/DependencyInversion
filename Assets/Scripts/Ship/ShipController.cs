using UnityEngine;

namespace GenericSpaceSim.Ship
{
    /// <summary>
    /// Creates a set of core systems instances to control the ship.
    /// </summary>
    public class ShipController : MonoBehaviour
    {
        [SerializeField] private MovementSettings movementSettings;
        [SerializeField] private RotationSettings rotationSettings;

        [SerializeField] private Transform targetShip;
        [SerializeField] private FloatReference floatRef;

        private ShipMotor shipMotor;
        private ShipRotator shipRotator;
        private ShipInput shipInput;
        private ShipCollisions shipCollisions;

        private float currentSpeed;

        public float CurrentSpeed => currentSpeed;

        private void Awake()
        {
            if (targetShip == null)
                targetShip = this.transform;

            shipCollisions = gameObject.AddComponent(typeof(ShipCollisions)) as ShipCollisions;

            shipInput = new ShipInput();
            shipMotor = new ShipMotor(movementSettings, shipInput, targetShip, shipCollisions);
            shipRotator = new ShipRotator(rotationSettings, shipInput, targetShip);
        }

        private void Update()
        {
            shipInput.HandleKeyboardInput();
            shipInput.HandleControllerInput();

            shipMotor.HandleMovement();
            shipMotor.ExposeSpeedInfo(ref currentSpeed);

            shipRotator.HandleRotation();
            shipRotator.HandleTilting();

            floatRef.ConstantValue = 10f;
        }
    }
}
