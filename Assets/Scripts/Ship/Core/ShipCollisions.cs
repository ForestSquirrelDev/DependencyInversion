using UnityEngine;
using System;

namespace GenericSpaceSim.Ship
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShipCollisions : MonoBehaviour
    {
        public event Action<float> OnCollisionOccured;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.rigidbody != null)
                OnCollisionOccured?.Invoke(collision.rigidbody.mass);
        }
    }
}
