using HttpPlayButtonStatus.Configuration;
using HttpPlayButtonStatus.Models;
using System;
using UnityEngine;
using Zenject;

namespace HttpPlayButtonStatus.Views
{
    public class GameViewController : IInitializable, IDisposable
    {
        private MicMuteController _micMuteController;
        private KeyEventController _keyEventController;
        private bool disposedValue;
        public GameObject _canvas;

        private GameViewController(MicMuteController micMuteCanvas, KeyEventController keyEventController)
        {
            this._micMuteController = micMuteCanvas;
            this._keyEventController = keyEventController;
        }

        public void Initialize()
        {
            if (!PluginConfig.Instance.MicMuteChangeEnable)
                return;
            this._canvas = this._micMuteController.NewCanvasCreate("GameMicMuteCanvas");
            this._canvas?.SetActive(this._micMuteController.GameIsMute);
            this._micMuteController.SendHttpStatus(this._micMuteController.GameIsMute);
            this._keyEventController.OnToggleEvent += this.OnToggleEvent;
            this._keyEventController.OnPushEvent += this.OnPushEvent;
        }
        public void OnToggleEvent()
        {
            this._micMuteController.GameIsMute = !this._micMuteController.GameIsMute;
            this._canvas?.SetActive(this._micMuteController.GameIsMute);
            this._micMuteController.SendHttpStatus(this._micMuteController.GameIsMute);
        }
        public void OnPushEvent(bool isPush)
        {
            bool isMute;
            if (isPush)
                isMute = !this._micMuteController.GameIsMute;
            else
                isMute = this._micMuteController.GameIsMute;
            this._canvas?.SetActive(isMute);
            this._micMuteController.SendHttpStatus(isMute);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                    if (PluginConfig.Instance.MicMuteChangeEnable)
                    {
                        this._micMuteController.SendHttpStatus(this._micMuteController.MenuIsMute);
                        UnityEngine.Object.Destroy(this._canvas);
                        this._keyEventController.OnToggleEvent -= this.OnToggleEvent;
                        this._keyEventController.OnPushEvent -= this.OnPushEvent;
                    }
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~GameViewController()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
