using UnityEngine;

namespace GenericSpaceSim.CameraManagement
{
    /// <summary>
    /// Key component of camera system. Responsible for switching cameras and passing referenced transforms to them.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Tooltip("Target ship to follow.")]
        [SerializeField] private Transform target;
        [Tooltip("Rig to attach camera into.")]
        [SerializeField] private Transform rig = null;

        [SerializeField] private AbstractCamera[] cameras;
        private CameraInput cameraInput;

        private int currentCamera = 0;

        private void Awake()
        {
            cameraInput = new CameraInput();
        }

        private void Update()
        {
            cameraInput.HandleKeyboardInput();
            LookForCameraChanging();
        }

        private void LateUpdate()
        {
            cameras[currentCamera].FollowPlayer(this.transform, target, rig);
        }

        private void LookForCameraChanging()
        {
            if(cameraInput.CIsPressed)
            {
                if (currentCamera != cameras.Length - 1)
                    currentCamera++;
                else
                    currentCamera = 0;
            }
        }
    }
}
