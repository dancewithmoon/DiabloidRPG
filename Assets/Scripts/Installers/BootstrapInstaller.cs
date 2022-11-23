using Constants;
using Model;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var playerData = Resources.Load<PlayerData>(Paths.PlayerData);
            Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
            Container.Bind<PlayerModel>().FromInstance(playerData.PlayerModel).AsSingle();

            SceneManager.LoadScene(Scenes.Gameplay);
        }
    }
}