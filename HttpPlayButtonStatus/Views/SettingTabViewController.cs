using HttpPlayButtonStatus.Configuration;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.GameplaySetup;
using Zenject;
using System.Globalization;
using HttpPlayButtonStatus.Models;
using System;
using System.ComponentModel;

namespace HttpPlayButtonStatus.Views
{
    internal class SettingTabViewController : IInitializable, IDisposable
    {
        private bool _disposedValue;
        public const string TabName = "Play Button Status";
        public string ResourceName => string.Join(".", GetType().Namespace, GetType().Name);
        private PlayButtonController _playButtonController;
        private MicMuteController _micMuteController;
        private MenuViewController _menuViewController;
        public SettingTabViewController(PlayButtonController playButtonController, MicMuteController micMuteController, MenuViewController menuViewController)
        {
            this._playButtonController = playButtonController;
            this._micMuteController = micMuteController;
            this._menuViewController = menuViewController;
        }

        public void Initialize()
        {
            GameplaySetup.instance.AddTab(TabName, this.ResourceName, this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    GameplaySetup.instance?.RemoveTab(TabName);
                }
                this._disposedValue = true;
            }
        }
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
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
        [UIValue("MicMuteChangeEnable")]
        public bool MicMuteChangeEnable
        {
            get => PluginConfig.Instance.MicMuteChangeEnable;
            set
            {
                PluginConfig.Instance.MicMuteChangeEnable = value;
                if (value)
                {
                    this._menuViewController._canvas?.SetActive(this._micMuteController.MenuIsMute);
                    this._micMuteController.SendHttpStatus(this._micMuteController.MenuIsMute);
                }
                else
                {
                    this._menuViewController._canvas?.SetActive(false);
                    this._micMuteController.SendHttpStatus(false);
                }
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
