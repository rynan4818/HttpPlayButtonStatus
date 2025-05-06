using HttpPlayButtonStatus.Models;
using Zenject;

namespace HttpPlayButtonStatus.Installers
{
    public class PlayButtonAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<PlayButtonController>().AsSingle();
            this.Container.BindInterfacesAndSelfTo<KeyEventController>().AsSingle();
            this.Container.BindInterfacesAndSelfTo<MicMuteController>().AsSingle();
        }
    }
}
