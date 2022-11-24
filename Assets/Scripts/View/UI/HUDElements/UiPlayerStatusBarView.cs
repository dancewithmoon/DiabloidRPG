using Model;
using UnityEngine;
using View.UI.UIElements;
using Zenject;

namespace View.UI
{
    public class UiPlayerStatusBarView : MonoBehaviour
    {
        [SerializeField] private ProgressBar _healthBar;
        
        private PlayerModel _playerModel;
        
        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            
            OnHealthUpdated();
            _playerModel.OnHealthUpdated += OnHealthUpdated;
        }
        
        private void OnHealthUpdated()
        {
            _healthBar.UpdateProgress(_playerModel.HealthCurrent, _playerModel.HealthMax);
        }

        private void OnDestroy()
        {
            _playerModel.OnHealthUpdated -= OnHealthUpdated;
        }
    }
}
