using Model;
using UnityEngine;
using Zenject;

namespace Behaviours.PlayerBehaviours
{
    public class Player : MonoBehaviour
    {
        private PlayerModel _playerModel;
        
        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
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
