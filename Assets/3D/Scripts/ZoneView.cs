using TMPro;
using UnityEngine;

namespace _3D.Scripts
{
    public class ZoneView : MonoBehaviour
    {
        [SerializeField] private TMP_Text zoneStatisticText;
        public static ZoneView Instance;

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        public void UpdateText(int value, int maxValue)
        {
            zoneStatisticText.text = $"{value}/{maxValue}";
        }
    }
}
