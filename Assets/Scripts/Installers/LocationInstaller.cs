using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [Header("Player")]
    public Transform startPoint;
    public GameObject playerPrefab;
    public float playerMovementSpeed;

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


    public override void InstallBindings()
    {
        BindSignals();
        BindInputs();
        BindPlayerParams();
        BindPlayer();
        BindUI();
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
            .InstantiatePrefabForComponent<Player>(playerPrefab, startPoint.position, Quaternion.identity, null);

        Container.Bind<Player>().FromInstance(player).AsSingle();
        Container.Bind<PlayerMovementTargetValidator>().FromComponentOn(player.gameObject).AsSingle();
        Container.Bind<CharacterAnimator>().FromComponentOn(player.gameObject).AsSingle();
        Container.Bind<PlayerMovement>().FromComponentOn(player.gameObject).AsSingle();
        Container.Bind<PlayerInterractionHandler>().FromComponentOn(player.gameObject).AsSingle();
    }

    private void BindPlayerParams()
    {
        Container.BindInstance(playerMovementSpeed).WhenInjectedInto<PlayerMovement>();
        Container.BindInstance(90).WithId("playerHealthCurrent").AsCached();
        Container.BindInstance(100).WithId("playerHealthMax").AsCached();
        Container.BindInstance(100).WithId("playerMana").AsCached();
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

        Container.DeclareSignal<UserTouchSignal>();
        Container.BindSignal<UserTouchSignal>().ToMethod<PlayerMovementTargetValidator>(validator => validator.ProcessTouch).FromResolve();

        Container.DeclareSignal<StartPlayerMovementSignal>();
        Container.BindSignal<StartPlayerMovementSignal>().ToMethod<CharacterAnimator>(view => view.Move).FromResolve();
        Container.BindSignal<StartPlayerMovementSignal>().ToMethod<PlayerMovement>(movement => movement.StartMoving).FromResolve();

        Container.DeclareSignal<StopPlayerMovementSignal>();
        Container.BindSignal<StopPlayerMovementSignal>().ToMethod<CharacterAnimator>(view => view.Stay).FromResolve();
        Container.BindSignal<StopPlayerMovementSignal>().ToMethod<PlayerMovement>(movement => movement.StopMoving).FromResolve();

        Container.DeclareSignal<ObstacleInterractionSignal>();
        Container.BindSignal<ObstacleInterractionSignal>().ToMethod<PlayerMovementTargetValidator>(validator => validator.ProcessObstacleInterraction).FromResolve();

        Container.DeclareSignal<ApplyPlayerDamageSignal>();
        Container.BindSignal<ApplyPlayerDamageSignal>().ToMethod<Player>(player => player.ApplyDamage).FromResolve();

        Container.DeclareSignal<UpdatePlayerHealthSignal>();
        Container.BindSignal<UpdatePlayerHealthSignal>().ToMethod<HealthPresenter>(presenter => presenter.UpdateHealth).FromResolve();

        Container.DeclareSignal<PlayerDiedSignal>();
        Container.BindSignal<PlayerDiedSignal>().ToMethod<CharacterAnimator>(view => view.Die).FromResolve();
        Container.BindSignal<PlayerDiedSignal>().ToMethod<TapInput>(input => input.Disable).FromResolve();
        Container.BindSignal<PlayerDiedSignal>().ToMethod<DeathScreen>(deathScreen => deathScreen.Show).FromResolve();
    }
}