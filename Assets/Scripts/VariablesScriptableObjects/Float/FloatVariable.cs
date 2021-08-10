using UnityEngine;

namespace GenericSpaceSim.Variables
{
    [CreateAssetMenu]
    public class FloatVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea(5, 5)]
        [SerializeField] private string DeveloperDescription = "";
#endif
        public float Value { get; set; }

        public static implicit operator float(FloatVariable f)
        {
            return f.Value;
        }
    }
}
