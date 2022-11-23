 using Behaviours.PlayerBehaviours;
using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    private Transform _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player.transform;
    }

    private void FixedUpdate()
    {
        if (IsCameraOnPlayer)
        {
            var target = _player.position;
            target.y = transform.position.y;
            target.x += 13;
            target.z += 2;
            transform.position = Vector3.Lerp(transform.position, target, _cameraSpeed * Time.fixedDeltaTime);
        }
    }

    private bool IsCameraOnPlayer => _player != null && Vector3.Distance(transform.position, _player.position) > 0.5f;
}
