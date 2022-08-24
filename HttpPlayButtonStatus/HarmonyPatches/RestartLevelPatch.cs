using HarmonyLib;
using System;

namespace HttpPlayButtonStatus.HarmonyPatches
{
    [HarmonyPatch(typeof(StandardLevelRestartController))]
    [HarmonyPatch("RestartLevel", MethodType.Normal)]
    public class RestartLevelPatch
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
