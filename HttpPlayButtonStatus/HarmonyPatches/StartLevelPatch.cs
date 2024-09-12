using HarmonyLib;
using System;

namespace HttpPlayButtonStatus.HarmonyPatches
{
    [HarmonyPatch(typeof(SinglePlayerLevelSelectionFlowCoordinator))]
    [HarmonyPatch(nameof(SinglePlayerLevelSelectionFlowCoordinator.StartLevel), MethodType.Normal)]
    public class StartLevelPatch
    {
        public static event Action<bool> OnStartLevel;
        public static string levelID;
        public static string songName;
        static bool Prefix(bool practice, SinglePlayerLevelSelectionFlowCoordinator __instance)
        {
            var previewBeatmapLevel = __instance.selectedBeatmapLevel;
            Plugin.Log.Info($"StartLevel:{previewBeatmapLevel.levelID}");
            levelID = previewBeatmapLevel.levelID;
            songName = previewBeatmapLevel.songName;
            OnStartLevel?.Invoke(false);
            return true;
        }
    }
}
