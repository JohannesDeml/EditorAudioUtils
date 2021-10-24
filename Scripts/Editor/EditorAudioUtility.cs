// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorAudioUtility.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JD.EditorAudioUtils
{
	public static class EditorAudioUtility
	{
		private static bool _initialized;
		private static MethodInfo _playClipMethod;
		private static object[] _callParameters;
		
		public static void PlaySound(EditorSoundType type)
		{
			PlayAudioClip(EditorAudioSettings.Instance.GetAudioClip(type));
		}

		public static void PlayAudioClip(AudioClip audioClip)
		{
			PlayAudioClip(audioClip, 0, false);
		}

		private static void PlayAudioClip(AudioClip audioClip, int startSample, bool loop)
		{
			if (!_initialized)
			{
				Initialize();
			}

			_callParameters[0] = audioClip;
			_callParameters[1] = startSample;
			_callParameters[2] = loop;
			_playClipMethod.Invoke(
				null,
				_callParameters
			);
		}

		private static void Initialize()
		{
			Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
			Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
			
			#if UNITY_2020_1_OR_NEWER
			string methodName = "PlayPreviewClip";
			#else
			string methodName = "PlayClip";
			#endif
			_playClipMethod = audioUtilClass.GetMethod(
				methodName,
				BindingFlags.Static | BindingFlags.Public,
				null,
				new Type[] { typeof(AudioClip), typeof(int), typeof(bool) },
				null
			);

			_callParameters = new object[3];
			_initialized = true;
		}
	}
}