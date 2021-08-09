using UnityEngine;

namespace GenericSpaceSim.CameraManagement
{
    /// <summary>
    /// Smoothly follows player from behind.
    /// </summary>
    [CreateAssetMenu(menuName = "Back camera")]
    public class BackCam : AbstractCamera
    {
        [Range(0.01f, 0.5f)]
        [Tooltip("How high above the ship should camera be placed.")]
        [SerializeField] private float cameraHeight = 0.25f;

        [Range(1.0f, 100.0f)]
        [Tooltip("How far from target will camera fly (Z world axis).")]
        [SerializeField] private float distance = 12.0f;

        [Range(1.0f, 10.0f)]
        [Tooltip("How fast camera changes its rotation according to player ship.")]
        [SerializeField] private float rotationSpeed = 3.0f;

        [Range(0.01f, 1.0f)]
        [Tooltip("Bigger value -> camera reaches player slower (Z world axis).")]
        [SerializeField] private float smoothTime = 0.2f;

        private Vector3 cameraPos;
        private Vector3 velocity;

        private float angle;

        public override void FollowPlayer(Transform thisTransform, Transform target, Transform rig = null)
        {
            // Calculate position.
            cameraPos = target.position - (target.forward * distance) + target.up * distance * cameraHeight;

            thisTransform.position = Vector3.SmoothDamp(thisTransform.position, cameraPos, ref velocity, smoothTime);

            // Calculate angle for rotation smoothing.
            angle = Mathf.Abs(Quaternion.Angle(thisTransform.rotation, target.rotation));

            thisTransform.rotation = Quaternion.RotateTowards(from: thisTransform.rotation,
                                                              to: target.rotation,
                                                              maxDegreesDelta: (angle * rotationSpeed) * Time.deltaTime);
        }
    }
}
