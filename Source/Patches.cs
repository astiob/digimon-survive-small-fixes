using HarmonyLib;
using RenderHeads.Media.AVProVideo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace SmallFixPlugin
{
	public class Patches
	{
		/// <summary>
		/// Unconditionally return the texture, so that
		/// <c>ApplyTextureProperties</c> actually gets a chance to do
		/// anything (and in particular to set <c>filterMode</c>).
		/// <para>
		/// Without this, <c>null</c> is returned, and the texture
		/// retains its initial value of 0 = <c>FilterMode.Point</c>,
		/// causing any lines/curves in the video to look terrible
		/// at horizontal resolutions other than 1920px.
		/// </para>
		/// </summary>
		[HarmonyPatch(typeof(WindowsMediaPlayer), "GetTexture")]
		[HarmonyPrefix]
		public static bool GetTexture_replace(Texture ____texture, ref Texture __result)
		{
			__result = ____texture;
			return false;
		}

		/// <summary>
		/// Tell the video player to release its handle on the
		/// video file before attempting to delete the file.
		/// <para>
		/// Without this, deleting the file may be forbidden (depending on
		/// details of the MP4 splitter/demuxer that ended up being used)
		/// as it is still in use, causing the game to crash-freeze when
		/// the video cutscene ends or is skipped.
		/// </para>
		/// </summary>
		[HarmonyPatch(typeof(uiMovie), "OnDestroy")]
		[HarmonyPatch(typeof(uiMovie), "UpdateCoroutine", MethodType.Enumerator)]
		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> uiMovie_transpiler(MethodBase original, IEnumerable<CodeInstruction> instructions)
		{
			foreach (var instruction in instructions)
			{
				if (instruction.Calls(AccessTools.Method(typeof(File), "Delete")))
				{
					yield return new CodeInstruction(original.DeclaringType == typeof(uiMovie) ? OpCodes.Ldarg_0 : OpCodes.Ldloc_1);
					yield return Transpilers.EmitDelegate<Action<uiMovie>>(self =>
					{
						var movie = self.GetComponentInChildren<sysuGuiMovie>();
						var player = movie.GetComponentInChildren<MediaPlayer>();
						player.CloseVideo();
					});
				}
				yield return instruction;
			}
		}
	}
}
