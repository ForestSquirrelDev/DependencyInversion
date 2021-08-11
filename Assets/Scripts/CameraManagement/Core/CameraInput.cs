using UnityEngine;

namespace GenericSpaceSim.CameraManagement
{
    public class CameraInput
    {
        public bool CIsPressed { get; private set; }

        public void HandleKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.C))
                CIsPressed = true;
            else
                CIsPressed = false;
        }
    }
}
