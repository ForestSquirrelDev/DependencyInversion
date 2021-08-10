using UnityEngine;

namespace GenericSpaceSim.CameraManagement
{
    public abstract class AbstractCamera : ScriptableObject
    {
        public abstract void FollowPlayer(Transform thisTransform, Transform target, Transform rig = null);
    }
}
