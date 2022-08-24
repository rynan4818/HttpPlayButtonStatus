using System;
using System.Text.RegularExpressions;
using HttpPlayButtonStatus.HarmonyPatches;
using HttpSiraStatus;
using HttpSiraStatus.Interfaces;
using HttpSiraStatus.Util;
using Zenject;
using static IPA.Logging.Logger;

namespace HttpPlayButtonStatus
{
    internal class PlayButtonController : IInitializable, IDisposable
    {
        private bool _disposedValue;
        private IStatusManager _statusManager;
        [Inject]
        public PlayButtonController(IStatusManager statusManager)
        {
            _statusManager = statusManager;
        }

        private void PlayStart(bool restart)
        {
            var rootObj = new JSONObject();
            rootObj["PlayStart"] = !restart;
            rootObj["Restart"] = restart;
            rootObj["LevelID"] = StartLevelPatch.levelID;
            rootObj["SongHash"] = Regex.IsMatch(StartLevelPatch.levelID, "^custom_level_[0-9A-F]{40}", RegexOptions.IgnoreCase) && !StartLevelPatch.levelID.EndsWith(" WIP") ? StartLevelPatch.levelID.Substring(13, 40) : null;
            this._statusManager.OtherJSON["HttpPlayButtonStatus"] = rootObj;
            this._statusManager.EmitStatusUpdate(ChangedProperty.Other, BeatSaberEvent.Other);
        }

        public void Initialize()
        {
            StartLevelPatch.OnStartLevel += PlayStart;
            RestartLevelPatch.OnRestartLevel += PlayStart;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    StartLevelPatch.OnStartLevel -= PlayStart;
                    RestartLevelPatch.OnRestartLevel -= PlayStart;
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
