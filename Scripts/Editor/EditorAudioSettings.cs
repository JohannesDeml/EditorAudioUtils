// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorAudioSettings.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace JD.EditorAudioUtils
{
	public enum EditorSoundType
	{
		Success,
		Warning,
		Error,
		Info,
		Notification
	}
	
	public class EditorAudioSettings : ScriptableObject
	{
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _successSound = null;
		
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _warningSound = null;
		
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _errorSound = null;
		
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _infoSound = null;
		
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _notificationSound = null;

		private static EditorAudioSettings _instance = null;

		public static EditorAudioSettings Instance
		{
			get
			{
				if (_instance == null)
				{
					Profiler.BeginSample($"GetScriptableSingletonInstance {typeof(EditorAudioSettings).FullName}");
					_instance = EditorUtilities.FindOrCreateEditorAsset<EditorAudioSettings>("EditorAudioUtils",
						$"{nameof(EditorAudioSettings)}.asset", true);
					Profiler.EndSample();
				}

				return _instance;
			}
		}

		public AudioClip GetAudioClip(EditorSoundType type)
		{
			switch (type)
			{
				case EditorSoundType.Success:
					return _successSound;
				case EditorSoundType.Warning:
					return _warningSound;
				case EditorSoundType.Error:
					return _errorSound;
				case EditorSoundType.Info:
					return _infoSound;
				case EditorSoundType.Notification:
					return _notificationSound;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}

	