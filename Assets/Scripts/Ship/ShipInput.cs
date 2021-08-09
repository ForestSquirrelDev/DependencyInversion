using UnityEngine;

namespace GenericSpaceSim.Ship
{
    public class ShipInput : IPlayerInput
    {
        private Vector3 mousePos;

        // Mouse position relative to screen.
        public float Pitch { get; private set; }
        public float Yaw { get; private set; }

        // Keyboard input info.
        public bool WIsPressed { get; private set; }
        public bool SIsPressed { get; private set; }
        public bool QIsPressed { get; private set; }
        public bool EIsPressed { get; private set; }

        public void HandleControllerInput()
        {
            mousePos = Input.mousePosition;

            // Figure out mouse position relative to center of screen.
            // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
            Pitch = (mousePos.y - Screen.height * 0.5f) / (Screen.height * 0.5f);
            Yaw = (mousePos.x - Screen.width * 0.5f) / (Screen.width * 0.5f);

            // Make sure the values don't exceed limits.
            Pitch = -Mathf.Clamp(Pitch, -1.0f, 1.0f);
            Yaw = Mathf.Clamp(Yaw, -1.0f, 1.0f);
        }

        public void HandleKeyboardInput()
        {
            if (Input.GetKey(KeyCode.W))
                WIsPressed = true;
            else
                WIsPressed = false;

            if (Input.GetKey(KeyCode.S))
                SIsPressed = true;
            else
                SIsPressed = false;

            if (Input.GetKey(KeyCode.Q))
                QIsPressed = true;
            else
                QIsPressed = false;

            if (Input.GetKey(KeyCode.E))
                EIsPressed = true;
            else
                EIsPressed = false;
        }
    }
}
