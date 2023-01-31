using BepInEx;
using HarmonyLib;

namespace VideoFixPlugin
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class VideoFixPlugin : BaseUnityPlugin
    {
        public void Awake()
        {
            Logger.LogInfo($"Plugin {pluginGuid} loaded");
            Harmony.CreateAndPatchAll(typeof(Patches));
        }

        public const string pluginGuid = "mods.digimonsurvive.videofix";
        public const string pluginName = "Video Fix Plugin";
        public const string pluginVersion = "1.0.0.0";
    }
}
