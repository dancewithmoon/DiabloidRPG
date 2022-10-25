using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class PlayerInterractionHandler : MonoBehaviour
{
    [Inject]
    public SignalBus SignalBus { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            SignalBus.Fire(new ObstacleInterractionSignal() { obstacleInterracted = obstacle });
        }
    }
}
