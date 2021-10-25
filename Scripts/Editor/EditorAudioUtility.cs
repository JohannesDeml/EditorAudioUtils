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
	/// <summary>
	/// Interface to play AudioClips and Notification sounds in the editor
	/// See also https://forum.unity.com/threads/way-to-play-audio-in-editor-using-an-editor-script.132042/
	/// </summary>
	public static class EditorAudioUtility
	{
		public static AudioClip LastPlayedPreviewClip { get; private set; }
		private static bool _initialized;
		private static AudioUtilMethodWrapper _playPreviewClip;
		private static AudioUtilMethodWrapper _pausePreviewClip;
		private static AudioUtilMethodWrapper _resumePreviewClip;
		private static AudioUtilMethodWrapper _stopPreviewClip;
		private static AudioUtilMethodWrapper _stopAllPreviewClips;
		private static AudioUtilMethodWrapper _isPreviewClipPlaying;
		
		/// <summary>
		/// Play a predefined notification sound, if the user enabled notification sounds
		/// </summary>
		/// <param name="type">Type of the sound to play</param>
		public static void PlayNotificationSound(EditorNotificationSound type)
		{
			if (EditorNotificationSettings.NotificationSoundsEnabled)
			{
				PlayPreviewClip(EditorNotificationSettings.Instance.GetAudioClip(type));
			}
		}

		/// <summary>
		/// Play an audio clip in the editor once
		/// </summary>
		/// <param name="audioClip">Audio clip to play</param>
		public static void PlayPreviewClip(AudioClip audioClip)
		{
			PlayPreviewClip(audioClip, 0, false);
		}

		/// <summary>
		/// Play an audio clip in the editor
		/// </summary>
		/// <param name="audioClip">Clip to play</param>
		/// <param name="startSample">The sample to start playing the clip</param>
		/// <param name="loop"></param>
		public static void PlayPreviewClip(AudioClip audioClip, int startSample, bool loop)
		{
			if (!_initialized)
			{
				Initialize();
			}

			LastPlayedPreviewClip = audioClip;
			_playPreviewClip.Invoke(audioClip, startSample, loop);
		}
		
		/// <summary>
		/// Pause the currently playing preview clips (Unity 2020+) or the defined clip (Unity 2019)
		/// </summary>
		/// <param name="audioClip">Clip to pause (Unity 2019) - Ignored in Unity 2020+</param>
		public static void PausePreviewClip(AudioClip audioClip)
		{
			if (!_initialized)
			{
				Initialize();
			}

			#if UNITY_2020_1_OR_NEWER
			_pausePreviewClip.Invoke();
			#else
			_pausePreviewClip.Invoke(audioClip);
			#endif
		}
		
		/// <summary>
		/// Resume the paused preview clips (Unity 2020+) or the defined clip (Unity 2019)
		/// </summary>
		/// <param name="audioClip">Clip to resume (Unity 2019) - Ignored in Unity 2020+</param>
		public static void ResumePreviewClip(AudioClip audioClip)
		{
			if (!_initialized)
			{
				Initialize();
			}

			#if UNITY_2020_1_OR_NEWER
			_resumePreviewClip.Invoke();
			#else
			_resumePreviewClip.Invoke(audioClip);
			#endif
		}
		
		/// <summary>
		/// Stop all preview clips (Unity 2020+) or the defined clip (Unity 2019)
		/// </summary>
		/// <param name="audioClip">Clip to stop (Unity 2019) - Ignored in Unity 2020+</param>
		public static void StopPreviewClip(AudioClip audioClip)
		{
			if (!_initialized)
			{
				Initialize();
			}

			#if UNITY_2020_1_OR_NEWER
			_stopPreviewClip.Invoke();
			#else
			_stopPreviewClip.Invoke(audioClip);
			#endif
		}
		
		/// <summary>
		/// Stop all playing preview clips
		/// </summary>
		public static void StopAllPreviewClips()
		{
			if (!_initialized)
			{
				Initialize();
			}
			
			_stopAllPreviewClips.Invoke();
		}

		/// <summary>
		/// Is any preview clip playing (Unity 2020+) or the defined clip playing (Unity 2019)
		/// </summary>
		/// <param name="audioClip">Clip to check if it is playing (Unity 2019) - Ignored in Unity 2020+</param>
		/// <returns>True if audioClip is playing (Unity 2019) or any clip is playing (Unity 2020+=</returns>
		public static bool IsPreviewClipPlaying(AudioClip audioClip)
		{
			if (!_initialized)
			{
				Initialize();
			}

			#if UNITY_2020_1_OR_NEWER
			return (bool)_isPreviewClipPlaying.Invoke();
			#else
			return (bool)_isPreviewClipPlaying.Invoke(audioClip);
			#endif
		}
		
		private static void Initialize()
		{
			Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
			Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
			
			#if UNITY_2020_1_OR_NEWER
			_playPreviewClip = new AudioUtilMethodWrapper(audioUtilClass, "PlayPreviewClip",
				new[] {typeof(AudioClip), typeof(int), typeof(bool)} );
			_pausePreviewClip = new AudioUtilMethodWrapper(audioUtilClass, "PausePreviewClip",
				Array.Empty<Type>());
			_resumePreviewClip = new AudioUtilMethodWrapper(audioUtilClass, "ResumePreviewClip",
				Array.Empty<Type>());
			_stopPreviewClip = new AudioUtilMethodWrapper(audioUtilClass, "StopAllPreviewClips",
				Array.Empty<Type>());
			_stopAllPreviewClips = new AudioUtilMethodWrapper(audioUtilClass, "StopAllPreviewClips",
				Array.Empty<Type>());
			_isPreviewClipPlaying = new AudioUtilMethodWrapper(audioUtilClass, "IsPreviewClipPlaying",
				Array.Empty<Type>());
			#else
			_playPreviewClip = new AudioUtilMethodWrapper(audioUtilClass, "PlayClip",
				new[] {typeof(AudioClip), typeof(int), typeof(bool)} );
			_pausePreviewClip = new AudioUtilMethodWrapper(audioUtilClass, "PauseClip",
				new[] {typeof(AudioClip)});
			_resumePreviewClip = new AudioUtilMethodWrapper(audioUtilClass, "ResumeClip",
				new[] {typeof(AudioClip)});
			_isPreviewClipPlaying = new AudioUtilMethodWrapper(audioUtilClass, "IsClipPlaying",
				new[] {typeof(AudioClip)});
			_stopPreviewClip = new AudioUtilMethodWrapper(audioUtilClass, "StopClip",
				new[] {typeof(AudioClip)});
			_stopAllPreviewClips = new AudioUtilMethodWrapper(audioUtilClass, "StopAllClips",
				Array.Empty<Type>());
			#endif
			
			_initialized = true;
		}
	}
}