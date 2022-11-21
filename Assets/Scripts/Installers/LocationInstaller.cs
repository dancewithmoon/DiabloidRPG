using Behaviours.PlayerBehaviours;
using Constants;
using Controllers;
using Model;
using ScriptableObjects;
using UnityEngine;
using UserInput;
using Zenject;

namespace Installers
{
    public class LocationInstaller : MonoInstaller
    {
        [Header("Player")]
        public Transform _playerSpawnPoint;

        [Space(5)]
        [Header("Inputs")]
        public TapInput userInputPrefab;
        public Transform userInputCanvas;

        [Space(5)]
        [Header("UI")]
        public Transform uiOverlayCanvas;
        public GameObject playerStatusBarPrefab;

        [Space(5)]
        [Header("Death Screen")]
        public Transform uiDeathScreenCanvas;
        public GameObject deathScreenPrefab;

        private PlayerData _playerData;
    

        public override void InstallBindings()
        {
            _playerData = Resources.Load<PlayerData>(Paths.PlayerData);

            BindSignals();
            BindData();
            BindPlayer();
            BindInputs();
            BindUI();
        }

        public void BindData()
        {
            Container.Bind<PlayerModel>().FromInstance(_playerData.PlayerModel);
        }
    
        public void BindInputs()
        {
            TapInput raycaster = Container
                .InstantiatePrefabForComponent<TapInput>(userInputPrefab, userInputCanvas);
            Container.Bind<TapInput>().FromInstance(raycaster).AsSingle();
        }   

        public void BindPlayer()
        {
            Player player = Container
                .InstantiatePrefabForComponent<Player>(_playerData.PlayerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);

            Container.Bind<Player>().FromInstance(player).AsSingle();
            Container.Bind<ITapReceiver>().FromInstance(player.GetComponent<ITapReceiver>());
            
            Container.Bind<CharacterAnimator>().FromComponentOn(player.gameObject).AsSingle();
            Container.Bind<PlayerInteractionHandler>().FromComponentOn(player.gameObject).AsSingle();
        }

        private void BindUI()
        {
            PlayerStatusBar statusBar = Container.InstantiatePrefabForComponent<PlayerStatusBar>(playerStatusBarPrefab, uiOverlayCanvas);
            Container.Bind<HealthPresenter>().FromInstance(statusBar.HealthPresenter).AsSingle();

            DeathScreen deathScreen = Container.InstantiatePrefabForComponent<DeathScreen>(deathScreenPrefab, uiDeathScreenCanvas);
            Container.Bind<DeathScreen>().FromInstance(deathScreen).AsSingle();
        }

        public void BindSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<StopPlayerMovementSignal>();
            Container.BindSignal<StopPlayerMovementSignal>().ToMethod<PlayerMovement>(movement => movement.StopMovement).FromResolve();

            Container.DeclareSignal<ApplyPlayerDamageSignal>();
            Container.BindSignal<ApplyPlayerDamageSignal>().ToMethod<Player>(player => player.ApplyDamage).FromResolve();

            Container.DeclareSignal<UpdatePlayerHealthSignal>();
            Container.BindSignal<UpdatePlayerHealthSignal>().ToMethod<HealthPresenter>(presenter => presenter.UpdateHealth).FromResolve();

            Container.DeclareSignal<PlayerDiedSignal>();
            Container.BindSignal<PlayerDiedSignal>().ToMethod<CharacterAnimator>(view => view.Die).FromResolve();
            Container.BindSignal<PlayerDiedSignal>().ToMethod<DeathScreen>(deathScreen => deathScreen.Show).FromResolve();
        }
    }
}