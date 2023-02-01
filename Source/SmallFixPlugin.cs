using BepInEx;
using HarmonyLib;

namespace SmallFixPlugin
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class SmallFixPlugin : BaseUnityPlugin
    {
        public void Awake()
        {
            Logger.LogInfo($"Plugin {pluginGuid} loaded");
            new Harmony(pluginGuid).PatchAll(typeof(Patches));
        }

        public const string pluginGuid = "mods.digimonsurvive.smallfixes";
        public const string pluginName = "Small Fix Plugin";
        public const string pluginVersion = "1.1.0.0";
    }
}
