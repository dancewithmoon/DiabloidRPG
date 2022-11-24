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

        public void ApplyDamage(int amount)
        {
            _playerModel.ApplyDamage(amount);
        }

        public void ApplyHealing(int healing)
        {
            _playerModel.ApplyHealing(healing);
        }
    }
}
