using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class HealthPresenter : MonoBehaviour
{
    private ProgressBar progressBar;

    private void Awake()
    {
        progressBar = GetComponent<ProgressBar>();
    }

    public void UpdateHealth(UpdatePlayerHealthSignal signal)
    {
        progressBar.UpdateProgress(signal.healthCurrent, signal.healthMax);
    }
}
