using UnityEngine;

namespace GenericSpaceSim.CameraManagement
{
    /// <summary>
    /// Simply places itself at rig position.
    /// </summary>
    [CreateAssetMenu(menuName = "Rig camera")]
    public class RigCam : AbstractCamera
    {
        public override void FollowPlayer(Transform thisTransform, Transform target, Transform rig = null)
        {
            if (rig != null)
            {
                thisTransform.position = rig.position;
                thisTransform.rotation = rig.rotation;
            }
            else
            {
                Debug.LogWarning("No rig attached to RigCamera");
            }
        }
    }
}
