using Behaviours.PlayerBehaviours;
using UnityEngine;

namespace Controllers
{
    public class PlayerTapReceiver : MonoBehaviour, ITapReceiver
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerInteractionHandler _playerInteractionHandler;
        
        public void Receive(GameObject target, Vector3 tapPosition)
        {
            if (_playerInteractionHandler.IsObstacleInteracted(target))
            {
                return;
            }
            
            _playerMovement.StartMovement(tapPosition);
            _playerInteractionHandler.ResetLastInteractedObstacle();
        }
    }
}