using Zenject;

namespace HttpPlayButtonStatus.Installers
{
    public class PlayButtonAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<PlayButtonController>().AsSingle();
        }
    }
}
