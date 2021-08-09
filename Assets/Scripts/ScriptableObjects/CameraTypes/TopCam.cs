using UnityEngine;

namespace GenericSpaceSim.CameraManagement
{
    /// <summary>
    /// Flies on top of the player ship.
    /// </summary>
    [CreateAssetMenu(menuName = "Top camera")]
    public class TopCam : AbstractCamera
    {
        [Range(1.0f, 100.0f)]
        [Tooltip("How far from and above the target will camera fly (both Y and Z axes).")]
        [SerializeField] private float distance = 30.0f;

        public override void FollowPlayer(Transform thisTransform, Transform target, Transform rig = null)
        {
            thisTransform.position = target.position + target.up * distance + target.forward * (distance / 3);
            thisTransform.rotation = target.rotation * Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
