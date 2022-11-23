using Model;
using UnityEngine;
using Zenject;

namespace Behaviours.PlayerBehaviours
{
    public class Player : MonoBehaviour
    {
        private PlayerModel _playerModel;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(PlayerModel playerModel, SignalBus signalBus)
        {
            _playerModel = playerModel;
            _signalBus = signalBus;
        }
    
        private void Start()
        {
            _signalBus.Fire(new UpdatePlayerHealthSignal
            {
                healthCurrent = _playerModel.HealthCurrent, 
                healthMax = _playerModel.HealthMax
            });
        }

        public void ApplyDamage(ApplyPlayerDamageSignal signal)
        {
            _playerModel.ApplyDamage(signal.damage);
        
            _signalBus.Fire(new UpdatePlayerHealthSignal
            {
                healthCurrent = _playerModel.HealthCurrent, 
                healthMax = _playerModel.HealthMax
            });
        
            if(_playerModel.HealthCurrent <= 0)
            {
                _signalBus.Fire(new PlayerDiedSignal());
            }
        }

        public void ApplyHealing(int healing)
        {
            _playerModel.ApplyHealing(healing);
        }
    }
}
