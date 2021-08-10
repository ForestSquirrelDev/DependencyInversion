using System;
using UnityEngine;

namespace GenericSpaceSim.Variables
{
    [Serializable]
    public class FloatReference
    {
        [SerializeField] private FloatVariable Variable;
        public float Value => Variable.Value;
    }
}
