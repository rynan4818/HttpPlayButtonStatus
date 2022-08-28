using HttpPlayButtonStatus.Views;
using Zenject;

namespace HttpPlayButtonStatus.Installers
{
    public class PlayButtonMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SettingTabViewController>().FromNewComponentAsViewController().AsSingle().NonLazy();
        }
    }
}
