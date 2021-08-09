using UnityEngine;
using UnityEngine.UI;
using GenericSpaceSim.Ship;

namespace GenericSpaceSim.UI
{
    /// <summary>
    /// Shows speed of player ship.
    /// </summary>
    public class Speedometer : MonoBehaviour
    {
        [SerializeField] private Text speedText;
        [SerializeField] private ShipController ship;

        private void Awake()
        {
            if (ship == null)
            {
                Debug.LogWarning($"Field '{nameof(ship)}' is not set in the inspector. FindObjectOfType will be used instead.");
                ship = FindObjectOfType<ShipController>();
            }
            if (speedText == null)
            {
                Debug.LogWarning($"Field '{nameof(speedText)}' is not set in the inspector. Using GetComponent<T>() instead.");
                transform.parent.GetComponentInChildren<Text>();
            }
        }

        void Update()
        {
            speedText.text = "Speed: " + ship.CurrentSpeed.ToString(format: "0");
        }
    }
}