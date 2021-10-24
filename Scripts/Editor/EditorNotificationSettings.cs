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
	public enum EditorNotificationSound
	{
		Success,
		Warning,
		Error,
		Info
	}
	
	public class EditorNotificationSettings : ScriptableObject
	{
		public static readonly string SoundsEnabledEditorPrefKey = "EditorAudioUtils.NotificationSoundsEnabled";
		
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

		[Tooltip("Default value for others opening the settings for the first time")]
		[SerializeField]
		public bool SoundsEnabledDefaultValue = true;

		private static EditorNotificationSettings _instance = null;

		public static EditorNotificationSettings Instance
		{
			get
			{
				if (_instance == null)
				{
					Profiler.BeginSample($"GetScriptableSingletonInstance {typeof(EditorNotificationSettings).FullName}");
					_instance = EditorUtilities.FindOrCreateEditorAsset<EditorNotificationSettings>("EditorAudioUtils",
						$"{nameof(EditorNotificationSettings)}.asset", true);
					Profiler.EndSample();
				}

				return _instance;
			}
		}

		public static bool NotificationSoundsEnabled
		{
			get => UnityEditor.EditorPrefs.GetBool(SoundsEnabledEditorPrefKey, Instance.SoundsEnabledDefaultValue);
			set => UnityEditor.EditorPrefs.SetBool(SoundsEnabledEditorPrefKey, value);
		}

		public AudioClip GetAudioClip(EditorNotificationSound type)
		{
			switch (type)
			{
				case EditorNotificationSound.Success:
					return _successSound;
				case EditorNotificationSound.Warning:
					return _warningSound;
				case EditorNotificationSound.Error:
					return _errorSound;
				case EditorNotificationSound.Info:
					return _infoSound;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}

	