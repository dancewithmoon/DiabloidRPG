using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Rigidbody playerBody;

    [Inject]
    public SignalBus SignalBus { get; set; }

    [Inject]
    public void Construct(float speed)
    {
        this.speed = speed;
    }

    public void StopMoving()
    {
        StopAllCoroutines();
    }

    public void StartMoving(StartPlayerMovementSignal signal)
    {
        StopMoving();

        var target = signal.targetPosition;
        target.y = transform.position.y;
        transform.LookAt(target);
        StartCoroutine(MoveTo(target));
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while (Vector2.Distance(VectorUtils.VectorXZtoXY(transform.position), VectorUtils.VectorXZtoXY(target)) > 0.1f)
        {
            var nextPosition = Vector3.MoveTowards(transform.position, target, speed);
            playerBody.MovePosition(nextPosition);
            yield return null;
        }
        SignalBus.Fire(new StopPlayerMovementSignal());
    }

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();   
    }
}