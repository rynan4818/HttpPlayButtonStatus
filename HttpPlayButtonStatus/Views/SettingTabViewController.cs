using HttpPlayButtonStatus.Configuration;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using Zenject;
using System.Globalization;
using HttpPlayButtonStatus.Models;

namespace HttpPlayButtonStatus.Views
{
    [HotReload]
    internal class SettingTabViewController : BSMLAutomaticViewController, IInitializable
    {
        public const string TabName = "Play Button Status";
        public string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);
        [Inject]
        private PlayButtonController _playButtonController;
        public void Initialize()
        {
            GameplaySetup.instance.AddTab(TabName, this.ResourceName, this);
        }
        protected override void OnDestroy()
        {
            GameplaySetup.instance.RemoveTab(TabName);
            base.OnDestroy();
        }
        [UIValue("PlayButtonEnable")]
        public bool PlayButtonEnable
        {
            get => PluginConfig.Instance.PlayButtonEnable;
            set
            {
                PluginConfig.Instance.PlayButtonEnable = value;
            }
        }
        [UIValue("SceneChangeEnable")]
        public bool SceneChangeEnable
        {
            get => PluginConfig.Instance.SceneChangeEnable;
            set
            {
                PluginConfig.Instance.SceneChangeEnable = value;
            }
        }
        [UIValue("PlayButtonDelay")]
        public float PlayButtonDelay
        {
            get => PluginConfig.Instance.PlayButtonDelay;
            set
            {
                PluginConfig.Instance.PlayButtonDelay = value;
            }
        }
        [UIValue("option-scene-name1")]
        public string OptionSceneName1
        {
            get => PluginConfig.Instance.OptionSceneName1;
        }
        [UIValue("option-scene-name2")]
        public string OptionSceneName2
        {
            get => PluginConfig.Instance.OptionSceneName2;
        }
        [UIValue("option-scene-name3")]
        public string OptionSceneName3
        {
            get => PluginConfig.Instance.OptionSceneName3;
        }
        [UIAction("OptionScene0")]
        private void OptionScene0()
        {
            _playButtonController.SceneChange(0);
        }
        [UIAction("OptionScene1")]
        private void OptionScene1()
        {
            _playButtonController.SceneChange(1);
        }
        [UIAction("OptionScene2")]
        private void OptionScene2()
        {
            _playButtonController.SceneChange(2);
        }
        [UIAction("OptionScene3")]
        private void OptionScene3()
        {
            _playButtonController.SceneChange(3);
        }
        [UIAction("PlayButtonDelayFormatter")]
        private string PlayButtonDelayFormatter(float value)
        {
            return $"{value.ToString("F2", CultureInfo.InvariantCulture)} s";
        }
    }
}
