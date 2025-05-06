using UnityEngine;
using System;
using Zenject;
using HttpPlayButtonStatus.Configuration;

namespace HttpPlayButtonStatus.Models
{
    public class KeyEventController : ITickable
    {
        public event Action OnToggleEvent;
        public event Action<bool> OnPushEvent;
        public void Tick()
        {
            if (!PluginConfig.Instance.MicMuteChangeEnable)
                return;
            if (Input.GetKey(KeyCode.JoystickButton14) && Input.GetKeyDown(KeyCode.JoystickButton15)) //左トリガー
            {
                this.OnToggleEvent.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton14)) //右トリガー
            {
                //this.OnPushEvent.Invoke(true);
            }
            if (Input.GetKeyUp(KeyCode.JoystickButton14)) //右トリガー
            {
                //this.OnPushEvent.Invoke(false);
            }
        }
    }
}
