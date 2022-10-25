using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform progressPlane;
    [SerializeField] private RectTransform progressFill;
    [SerializeField] private TextMeshProUGUI progressText;
    private float maxWidth;

    private void Awake()
    {
        maxWidth = progressPlane.rect.width;
    }

    public void UpdateProgress(int currentValue, int maxValue)
    {
        float progress =  (float)currentValue / maxValue;
        progressFill.sizeDelta = new Vector2(progress * maxWidth, progressFill.sizeDelta.y);
        progressText.text = currentValue + " / " + maxValue;
    }
}
