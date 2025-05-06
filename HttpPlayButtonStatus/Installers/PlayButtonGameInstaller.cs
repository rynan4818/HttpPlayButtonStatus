using HttpPlayButtonStatus.Models;
using HttpPlayButtonStatus.Views;
using Zenject;

namespace HttpPlayButtonStatus.Installers
{
    public class PlayButtonGameInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<GameSceneController>().AsSingle().NonLazy();
            this.Container.BindInterfacesAndSelfTo<GameViewController>().AsSingle().NonLazy();
        }
    }
}
