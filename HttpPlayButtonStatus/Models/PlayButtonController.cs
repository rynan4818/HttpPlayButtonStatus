using HttpPlayButtonStatus.Configuration;
using System;
using System.Text.RegularExpressions;
using HttpPlayButtonStatus.HarmonyPatches;
using HttpSiraStatus.Enums;
using HttpSiraStatus.Interfaces;
using HttpSiraStatus.Util;
using Zenject;

namespace HttpPlayButtonStatus.Models
{
    internal class PlayButtonController : IInitializable, IDisposable
    {
        private bool _disposedValue;
        private IStatusManager _statusManager;
        [Inject]
        public PlayButtonController(IStatusManager statusManager)
        {
            this._statusManager = statusManager;
        }

        public void PlayStart(bool menuScene)
        {
            var rootObj = new JSONObject();
            rootObj["PlayStart"] = PluginConfig.Instance.PlayButtonEnable ? !menuScene : false;
            rootObj["MenuScene"] = menuScene;
            rootObj["PlayButton"] = PluginConfig.Instance.PlayButtonEnable;
            rootObj["SceneChange"] = PluginConfig.Instance.SceneChangeEnable;
            rootObj["PlayButtonDelay"] = PluginConfig.Instance.PlayButtonDelay;
            rootObj["LevelID"] = StartLevelPatch.levelID;
            rootObj["SongName"] = StartLevelPatch.songName;
            if (StartLevelPatch.levelID != null)
                rootObj["SongHash"] = Regex.IsMatch(StartLevelPatch.levelID, "^custom_level_[0-9A-F]{40}", RegexOptions.IgnoreCase) && !StartLevelPatch.levelID.EndsWith(" WIP") ? StartLevelPatch.levelID.Substring(13, 40) : null;
            this._statusManager.OtherJSON["HttpPlayButtonStatus"] = rootObj;
            this._statusManager.EmitStatusUpdate(ChangedProperty.Other, BeatSaberEvent.Other);
        }

        public void SceneChange(int sceneNo)
        {
            var rootObj = new JSONObject();
            rootObj["OptionScene"] = sceneNo;
            this._statusManager.OtherJSON["HttpPlayButtonStatus"] = rootObj;
            this._statusManager.EmitStatusUpdate(ChangedProperty.Other, BeatSaberEvent.Other);
        }

        public void Initialize()
        {
            StartLevelPatch.OnStartLevel += PlayStart;
            HandlePauseMenuManagerDidPressRestartButtonPatch.OnRestartLevel += PlayStart;
            HandlePauseMenuManagerDidPressMenuButtonPatch.OnReturnMenu += PlayStart;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    StartLevelPatch.OnStartLevel -= PlayStart;
                    HandlePauseMenuManagerDidPressRestartButtonPatch.OnRestartLevel -= PlayStart;
                    HandlePauseMenuManagerDidPressMenuButtonPatch.OnReturnMenu -= PlayStart;
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
    }
}
