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
		/// Without this, deleting the file may be forbidden (depending
		/// on details of the MP4 splitter/demuxer that ended up
		/// being used) as it is still in use, causing the game to
		/// crash-freeze when the video cutscene ends or is skipped.
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

		/// <summary>
		/// Skip <c>ContentMove</c> if it is going to divide by zero.
		/// <para>
		/// Without this, opening Menu -> Profile at a 16:10 display
		/// resolution when exactly seven protagonists are known
		/// (exactly as many as fits on screen)--namely, after meeting
		/// Miu but before meeting Kaito--breaks the Profile menu until
		/// the whole game is restarted: it displays Takuma's portrait
		/// and Karma compass, but the list of protagonists is empty,
		/// and while it does react to keyboard/controller buttons
		/// and thus allows opening and switching profiles,
		/// the portrait/compass doesn't change.
		/// </para>
		/// <para>
		/// Curiously, this does not happen at 16:9. This is because
		/// the nested ScrollRect's bounds calculations give an exact
		/// zero at 16:9, resulting in an eventual 0*Inf = NaN that
		/// happens to hit a condition that unintentionally guards
		/// against NaN, whereas at 16:10 they give a small nonzero
		/// number, causing infinities to propagate forever.
		/// </para>
		/// </summary>
		[HarmonyPatch(typeof(ScrollViewVerticalBase), "ContentMove")]
		[HarmonyPrefix]
		public static bool ContentMove_guard(RectTransform ____content, RectTransform ____viewport)
		{
			float denominator = ____content.rect.height - ____viewport.rect.height;
			return denominator != 0;
		}
	}
}
