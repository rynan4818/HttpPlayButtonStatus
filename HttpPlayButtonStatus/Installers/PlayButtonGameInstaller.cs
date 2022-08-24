using Zenject;

namespace HttpPlayButtonStatus.Installers
{
    public class PlayButtonGameInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<GameSceneController>().AsSingle().NonLazy();
        }

    }
}
