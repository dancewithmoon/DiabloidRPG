using TMPro;
using UnityEngine;

namespace View.UI.UIElements
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _progressPlane;
        [SerializeField] private RectTransform _progressFill;
        [SerializeField] private TextMeshProUGUI _progressText;
        private float MaxWidth => _progressPlane.rect.width;
        
        public void UpdateProgress(int currentValue, int maxValue)
        {
            float progress =  (float)currentValue / maxValue;
            _progressFill.sizeDelta = new Vector2(progress * MaxWidth, _progressFill.sizeDelta.y);
            _progressText.text = currentValue + " / " + maxValue;
        }
    }
}
