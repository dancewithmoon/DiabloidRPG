using System;
using Behaviours.PlayerBehaviours;
using UnityEngine;
using UserInput;
using Zenject;

namespace Controllers
{
    public class PlayerTouchReceiver : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerInteractionHandler _playerInteractionHandler;
        private ITouchInput _touchInput;
        
        [Inject]
        private void Construct(ITouchInput touchInput)
        {
            _touchInput = touchInput;
            _touchInput.OnTouch += OnTouch;
        }

        private void OnTouch(GameObject target, Vector3 tapPosition)
        {
            if (_playerInteractionHandler.IsObstacleInteracted(target))
            {
                return;
            }
            
            _playerMovement.StartMovement(tapPosition);
            _playerInteractionHandler.ResetLastInteractedObstacle();
        }

        private void OnDestroy()
        {
            _touchInput.OnTouch -= OnTouch;
        }
    }
}