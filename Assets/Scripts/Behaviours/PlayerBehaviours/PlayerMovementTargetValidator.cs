using UnityEngine;
using Zenject;

public class PlayerMovementTargetValidator : MonoBehaviour
{
    private Obstacle lastInterractedObstacle;

    [Inject]
    public SignalBus SignalBus { get; set; }

    public void ProcessTouch(UserTouchSignal signal)
    {
        Vector3 touchPosition = signal.touchPosition;
        GameObject touchedObject = signal.touchedObject;
        
        if (CanMove(touchedObject))
        {
            SignalBus.Fire(new StartPlayerMovementSignal() { targetPosition = touchPosition });
        }
        else
        {
            lastInterractedObstacle = null;
        }
    }

    public void ProcessObstacleInterraction(ObstacleInterractionSignal signal)
    {
        lastInterractedObstacle = signal.obstacleInterracted;
        SignalBus.Fire(new StopPlayerMovementSignal());
    }

    private bool CanMove(GameObject touchedObject)
    {
        if (touchedObject.TryGetComponent(out Obstacle obstacle))
        {
            return obstacle != lastInterractedObstacle;
        }
        return true;
    }
}
