using BepInEx;
using HarmonyLib;
using System.Globalization;

namespace SmallFixPlugin
{
	[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
	public class SmallFixPlugin : BaseUnityPlugin
	{
		public void Awake()
		{
			Logger.LogInfo($"Plugin {pluginGuid} loaded");
			new Harmony(pluginGuid).PatchAll(typeof(Patches));
			FixCulture();
		}

		public const string pluginGuid = "mods.digimonsurvive.smallfixes";
		public const string pluginName = "Small Fix Plugin";
		public const string pluginVersion = "1.1.0.0";

		/// <summary>
		/// Make <c>ToLower()</c> always behave the same regardless
		/// of Windows regional settings, so that the game doesn't
		/// mess up paths to asset bundle files and fail to load them.
		/// <para>
		/// Without this, if the regional settings (date/time/currency
		/// format) are set to Turkish, the game gets stuck during
		/// startup at a fully black window/screen (after the window is
		/// created but before the splash screen "Loading..." label is
		/// shown) and doesn't work at all. This is because it badly
		/// converts some 'I's to small dotless 'i's, whereas the
		/// actual asset bundle files are named with small dotted 'i's.
		/// </para>
		/// <para>
		/// Ideally, the game should use <c>ToLowerInvariant()</c>,
		/// explicitly set a culture, or use correctly-cased names to
		/// begin with and thus avoid the need to convert case at all.
		/// But there are multiple places that call <c>ToLower()</c>,
		/// so this is both the easiest and the most reliable way
		/// to fix them all at once.
		/// </para>
		/// <para>
		/// To be clear, the game explicitly performs its own case
		/// conversion in <c>AssetBundleManager</c> and elsewhere,
		/// and it is this custom code that mistakenly calls the
		/// culture-dependent <c>ToLower()</c>. This issue is not
		/// just about case-insensitive file systems.
		/// </para>
		/// </summary>
		public static void FixCulture()
		{
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
		}
	}
}
