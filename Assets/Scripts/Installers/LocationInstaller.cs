using Behaviours.PlayerBehaviours;
using Constants;
using Controllers;
using ScriptableObjects;
using UnityEngine;
using UserInput;
using View.UI;
using Zenject;

namespace Installers
{
    public class LocationInstaller : MonoInstaller
    {
        [Inject] public PlayerData PlayerData { get; set; }

        [Header("Player")]
        [SerializeField] private Transform _playerSpawnPoint;

        public override void InstallBindings()
        {
            BindSignals();
            BindPlayer();
            BindInputs();
            BindUI();
        }

        private void BindInputs()
        {
            var tapInput = Container.InstantiatePrefabResourceForComponent<TapInput>(Paths.InputClickHandler);
            Container.Bind<TapInput>().FromInstance(tapInput).AsSingle();
        }   

        private void BindPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<Player>(PlayerData.PlayerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);

            Container.Bind<Player>().FromInstance(player).AsSingle();
            Container.Bind<ITapReceiver>().FromInstance(player.GetComponent<ITapReceiver>());
            
            Container.Bind<CharacterAnimator>().FromComponentOn(player.gameObject).AsSingle();
            Container.Bind<PlayerInteractionHandler>().FromComponentOn(player.gameObject).AsSingle();
        }

        private void BindUI()
        {
            var uiRoot = Container.InstantiatePrefabResourceForComponent<UIRoot>(Paths.UiRoot);
            Container.Bind<UIRoot>().FromInstance(uiRoot).AsSingle();
            Container.Bind<HealthPresenter>().FromInstance(uiRoot.GetComponentInChildren<HealthPresenter>());
            Container.Bind<DeathScreen>().FromInstance(uiRoot.GetComponentInChildren<DeathScreen>());
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