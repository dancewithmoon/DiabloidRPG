using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    private Transform player;

    [Inject]
    private void Construct(PlayerMovementTargetValidator player)
    {
        this.player = player.transform;
    }

    private void FixedUpdate()
    {
        if (IsCameraOnPlayer)
        {
            var target = player.position;
            target.y = transform.position.y;
            target.x += 13;
            target.z += 2;
            transform.position = Vector3.Lerp(transform.position, target, cameraSpeed * Time.fixedDeltaTime);
        }
    }

    private bool IsCameraOnPlayer => player != null && Vector3.Distance(transform.position, player.position) > 0.5f;
}
