namespace GenericSpaceSim.Core
{
    public interface IShipMotor
    {
        public void HandleMovement();
        public void HandleRotation();
        public void HandleTilting();
        public void ExposeSpeedInfo(ref float currentSpeed, ref float currentRollSpeed);
    }
}
