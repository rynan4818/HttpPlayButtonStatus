using HttpPlayButtonStatus.Models;
using System;
using UnityEngine;
using Zenject;

namespace HttpPlayButtonStatus.Views
{
    public class MenuViewController : IInitializable, IDisposable
    {
        private MicMuteController _micMuteController;
        private KeyEventController _keyEventController;
        private bool disposedValue;
        public GameObject _canvas;

        private MenuViewController(MicMuteController micMuteCanvas, KeyEventController keyEventController)
        {
            this._micMuteController = micMuteCanvas;
            this._keyEventController = keyEventController;
        }

        public void Initialize()
        {
            this._canvas = this._micMuteController.NewCanvasCreate("MenuMicMuteCanvas");
            this._canvas?.SetActive(this._micMuteController.MenuIsMute);
            this._micMuteController.SendHttpStatus(this._micMuteController.MenuIsMute);
            this._keyEventController.OnToggleEvent += this.OnToggleEvent;
            this._keyEventController.OnPushEvent += this.OnPushEvent;
        }
        public void OnToggleEvent()
        {
            this._micMuteController.MenuIsMute = !this._micMuteController.MenuIsMute;
            this._canvas?.SetActive(this._micMuteController.MenuIsMute);
            this._micMuteController.SendHttpStatus(this._micMuteController.MenuIsMute);
        }
        public void OnPushEvent(bool isPush)
        {
            bool isMute;
            if (isPush)
                isMute = !this._micMuteController.MenuIsMute;
            else
                isMute = this._micMuteController.MenuIsMute;
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
                    UnityEngine.Object.Destroy(this._canvas);
                    this._keyEventController.OnToggleEvent -= this.OnToggleEvent;
                    this._keyEventController.OnPushEvent -= this.OnPushEvent;
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~MenuViewController()
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
