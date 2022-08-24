using HarmonyLib;
using System;
using System.Reflection;

namespace HttpPlayButtonStatus.HarmonyPatches
{
    [HarmonyPatch(typeof(SinglePlayerLevelSelectionFlowCoordinator))]
    [HarmonyPatch("StartLevel", MethodType.Normal)]
    public class StartLevelPatch
    {
        public static event Action<bool> OnStartLevel;
        public static string levelID;
        static bool Prefix(bool practice, SinglePlayerLevelSelectionFlowCoordinator __instance)
        {
            var type = __instance.GetType();
            var property = type.GetProperty("selectedBeatmapLevel", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);
            var previewBeatmapLevel = (IPreviewBeatmapLevel)(property.GetValue(__instance));
            Plugin.Log.Info($"StartLevel:{previewBeatmapLevel.levelID}");
            levelID = previewBeatmapLevel.levelID;
            OnStartLevel?.Invoke(false);
            return true;
        }
    }
}
