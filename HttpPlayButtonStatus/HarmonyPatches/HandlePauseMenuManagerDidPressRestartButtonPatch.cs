using HarmonyLib;
using System;

namespace HttpPlayButtonStatus.HarmonyPatches
{
    [HarmonyPatch(typeof(PauseController))]
    [HarmonyPatch("HandlePauseMenuManagerDidPressRestartButton", MethodType.Normal)]
    public class HandlePauseMenuManagerDidPressRestartButtonPatch
    {
        public static event Action<bool> OnRestartLevel;
        static bool Prefix()
        {
            Plugin.Log.Info("RestartLevel");
            OnRestartLevel?.Invoke(true);
            return true;
        }
    }
}
