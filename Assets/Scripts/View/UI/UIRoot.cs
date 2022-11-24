using Model;
using UnityEngine;
using View.UI.Screens;
using Zenject;

namespace View.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private DeathScreen _deathScreen;
        
        private PlayerModel _playerModel;

        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;

            _playerModel.OnPlayerDead += OnPlayerDead;
        }

        private void OnPlayerDead()
        {
            _deathScreen.Show();
        }

        private void OnDestroy()
        {
            _playerModel.OnPlayerDead -= OnPlayerDead;
        }
    }
}
