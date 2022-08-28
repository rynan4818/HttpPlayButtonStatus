using HarmonyLib;
using System;

namespace HttpPlayButtonStatus.HarmonyPatches
{
    [HarmonyPatch(typeof(PauseController))]
    [HarmonyPatch("HandlePauseMenuManagerDidPressMenuButton", MethodType.Normal)]
    public class HandlePauseMenuManagerDidPressMenuButtonPatch
    {
        public static event Action<bool> OnReturnMenu;
        static bool Prefix()
        {
            Plugin.Log.Info("Return to Menu");
            OnReturnMenu?.Invoke(true);
            return true;
        }
    }
}
