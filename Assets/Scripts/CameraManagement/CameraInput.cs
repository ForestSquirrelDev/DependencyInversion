using UnityEngine;

namespace GenericSpaceSim.CameraManagement
{
    public class CameraInput : IPlayerInput
    {
        public bool CIsPressed { get; private set; }

        public void HandleControllerInput() => Debug.LogWarning("No controller input implementation.");

        public void HandleKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.C))
                CIsPressed = true;
            else
                CIsPressed = false;
        }
    }
}
