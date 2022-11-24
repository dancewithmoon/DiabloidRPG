using System.Diagnostics.CodeAnalysis;
using Behaviours.PlayerBehaviours;
using Constants;
using ScriptableObjects;
using UnityEngine;
using UserInput;
using View.UI;
using View.UI.Screens;
using Zenject;

namespace Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;

        [Header("Player")]
        [SerializeField] private Transform _playerSpawnPoint;

        private PlayerData _playerData;
        
        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public override void InstallBindings()
        {
            BindMainCamera();
            BindInputs();
            BindPlayer();
            BindUI();
        }

        private void BindMainCamera()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
        }
        
        private void BindInputs()
        {
            var tapInput = Container.InstantiatePrefabResourceForComponent<RaycastTouchInput>(Paths.InputClickHandler);
            Container.Bind<ITouchInput>().FromInstance(tapInput).AsSingle();
        }   

        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<Player>(_playerData.PlayerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);

            Container.Bind<Player>().FromInstance(player).AsSingle();
            Container.Bind<CharacterAnimator>().FromComponentOn(player.gameObject).AsSingle();
            Container.Bind<PlayerInteractionHandler>().FromComponentOn(player.gameObject).AsSingle();
        }

        private void BindUI()
        {
            var uiRoot = Container.InstantiatePrefabResourceForComponent<UIRoot>(Paths.UiRoot);
            Container.Bind<UIRoot>().FromInstance(uiRoot).AsSingle();
            Container.Bind<DeathScreen>().FromInstance(uiRoot.GetComponentInChildren<DeathScreen>()).AsSingle();
        }
    }
}