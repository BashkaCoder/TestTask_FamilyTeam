using _3D.Scripts.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3D.Scripts.UI
{
    public class SatietyBar : MonoBehaviour
    {
        [SerializeField] private Satiety _satiety;
        [SerializeField] private Image _fill;
        [SerializeField] private TMP_Text _satietyText;

        private void Start()
        {
            UpdateBar();
        }

        public void UpdateBar()
        {
            SetBarText(_satiety.SatietyPoints, _satiety.MaxSatietyPoints);
            SetBarImageFill(_satiety.SatietyPoints / _satiety.MaxSatietyPoints);
        }

        private void SetBarText(float currentValue, float maxValue)
        {
            _satietyText.text = $"{(int)currentValue} / {(int)maxValue}";
        }

        private void SetBarImageFill(float newValue)
        {
            _fill.fillAmount = newValue;
        }
    }
}
