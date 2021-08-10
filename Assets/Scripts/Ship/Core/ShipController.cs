using UnityEngine;
using GenericSpaceSim.Variables;

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
        [SerializeField] private FloatVariable shipSpeed;

        private ShipMotor shipMotor;
        private ShipRotator shipRotator;
        private ShipInput shipInput;
        private ShipCollisions shipCollisions;

        private void Awake()
        {
            if (targetShip == null)
                targetShip = this.transform;

            shipCollisions = gameObject.AddComponent(typeof(ShipCollisions)) as ShipCollisions;

            shipInput = new ShipInput();
            shipMotor = new ShipMotor(movementSettings, shipSpeed, shipInput, targetShip, shipCollisions);
            shipRotator = new ShipRotator(rotationSettings, shipInput, targetShip);
        }

        private void Update()
        {
            shipInput.HandleKeyboardInput();
            shipInput.HandleControllerInput();

            shipMotor.HandleMovement();

            shipRotator.HandleRotation();
            shipRotator.HandleTilting();
        }

        private void OnDisable()
        {
            shipSpeed.Value = 0;
        }
    }
}
