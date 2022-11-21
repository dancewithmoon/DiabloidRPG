using UnityEngine;

namespace Behaviours.PlayerBehaviours
{
    [RequireComponent(typeof(Collider))]
    public class PlayerInteractionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        private Obstacle _lastInteractedObstacle;

        public bool IsObstacleInteracted(GameObject touchedObject)
        {
            if (touchedObject.TryGetComponent(out Obstacle obstacle))
            {
                return obstacle == _lastInteractedObstacle;
            }
            return false;
        }

        public void ResetLastInteractedObstacle()
        {
            _lastInteractedObstacle = null;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
            {
                OnObstacleInteracted(obstacle);
            }
        }

        private void OnObstacleInteracted(Obstacle obstacle)
        {
            _lastInteractedObstacle = obstacle;
            _playerMovement.StopMovement();
        }
    }
}
