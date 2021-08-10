using UnityEngine;
using UnityEngine.UI;
using GenericSpaceSim.Variables;

namespace GenericSpaceSim.UI
{
    /// <summary>
    /// Shows speed of player ship.
    /// </summary>
    public class Speedometer : MonoBehaviour
    {
        [SerializeField] private Text speedText;
        [SerializeField] private FloatReference floatReference;

        private void Awake()
        {
            if (speedText == null)
            {
                Debug.LogWarning($"Field '{nameof(speedText)}' is not set in the inspector. Using GetComponent<T>() instead.");
                transform.parent.GetComponentInChildren<Text>();
            }
        }

        void Update()
        {
            speedText.text = "Speed: " + floatReference.Value.ToString(format: "0");
        }
    }
}