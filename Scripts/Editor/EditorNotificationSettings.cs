﻿// --------------------------------------------------------------------------------------------------------------------
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
		Info,
		CompileSuccess,
		CompileError,
		BuildSuccess,
		BuildError,
	}
	
	/// <summary>
	/// Holds the settings for custom notification sounds in the editor
	/// Those notifications are stored in a singleton and can be easily accessed through ProjectSettings -> EditorAudioUtils
	/// </summary>
	public class EditorNotificationSettings : ScriptableObject
	{
		public static readonly string SoundsEnabledEditorPrefKey = "EditorAudioUtils.NotificationSoundsEnabled";
		
		[Header("Project settings")]
		[Tooltip("Default value for success sound if not overwritten by the user")]
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _successSound = null;
		
		[Tooltip("Default value for warning sound if not overwritten by the user")]
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _warningSound = null;
		
		[Tooltip("Default value for error sound if not overwritten by the user")]
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _errorSound = null;
		
		[Tooltip("Default value for info sound if not overwritten by the user")]
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _infoSound = null;
		
		[Tooltip("Default value for successful compilation sound if not overwritten by the user")]
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _compileSuccess = null;
		
		[Tooltip("Default value for failed compilation sound if not overwritten by the user")]
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _compileError = null;
		
		[Tooltip("Default value for successful build sound if not overwritten by the user")]
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _buildSuccess = null;
		
		[Tooltip("Default value for failed build sound if not overwritten by the user")]
		[SerializeField]
		[PreviewAudioClip]
		private AudioClip _buildError = null;
		
		[Tooltip("Default value for notification sounds being enabled if not overwritten by the user")]
		[SerializeField]
		private bool _enableNotificationSounds = true;


		[Header("User Overwrites")]
		[SerializeField]
		private EditorPrefAudioClip _userSuccessSound = new EditorPrefAudioClip("EditorAudioUtils.UserSuccessSound");
		
		[SerializeField]
		private EditorPrefAudioClip _userWarningSound = new EditorPrefAudioClip("EditorAudioUtils.UserWarningSound");
		
		[SerializeField]
		private EditorPrefAudioClip _userErrorSound = new EditorPrefAudioClip("EditorAudioUtils.UserErrorSound");
		
		[SerializeField]
		private EditorPrefAudioClip _userInfoSound = new EditorPrefAudioClip("EditorAudioUtils.UserInfoSound");
		
		[SerializeField]
		private EditorPrefAudioClip _userCompileSuccessSound = new EditorPrefAudioClip("EditorAudioUtils.UserCompileSuccessSound");
		
		[SerializeField]
		private EditorPrefAudioClip _userCompileErrorSound = new EditorPrefAudioClip("EditorAudioUtils.UserCompileErrorSound");
				
		[SerializeField]
		private EditorPrefAudioClip _userBuildSuccessSound = new EditorPrefAudioClip("EditorAudioUtils.UserBuildSuccessSound");
		
		[SerializeField]
		private EditorPrefAudioClip _userBuildErrorSound = new EditorPrefAudioClip("EditorAudioUtils.UserBuildErrorSound");
		
		private static EditorNotificationSettings _instance = null;

		public static EditorNotificationSettings Instance
		{
			get
			{
				if (_instance == null)
				{
					Profiler.BeginSample($"GetScriptableSingletonInstance {typeof(EditorNotificationSettings).FullName}");
					_instance = EditorUtilities.FindOrCreateEditorAsset<EditorNotificationSettings>("EditorAudioUtils",
						"EditorNotificationSettings.asset", true);
					Profiler.EndSample();
				}

				return _instance;
			}
		}

		public static bool NotificationSoundsEnabled
		{
			get => UnityEditor.EditorPrefs.GetBool(SoundsEnabledEditorPrefKey, Instance._enableNotificationSounds);
			set => UnityEditor.EditorPrefs.SetBool(SoundsEnabledEditorPrefKey, value);
		}

		public AudioClip GetAudioClip(EditorNotificationSound type)
		{
			switch (type)
			{
				case EditorNotificationSound.Success:
					return _userSuccessSound.Clip != null ? _userSuccessSound.Clip : _successSound;
				case EditorNotificationSound.Warning:
					return _userWarningSound.Clip != null ? _userWarningSound.Clip : _warningSound;
				case EditorNotificationSound.Error:
					return _userErrorSound.Clip != null ? _userErrorSound.Clip : _errorSound;
				case EditorNotificationSound.Info:
					return _userInfoSound.Clip != null ? _userInfoSound.Clip : _infoSound;
				case EditorNotificationSound.CompileSuccess:
					return _userCompileSuccessSound.Clip != null ? _userCompileSuccessSound.Clip : _compileSuccess;
				case EditorNotificationSound.CompileError:
					return _userCompileErrorSound.Clip != null ? _userCompileErrorSound.Clip : _compileError;
				case EditorNotificationSound.BuildSuccess:
					return _userBuildSuccessSound.Clip != null ? _userBuildSuccessSound.Clip : _buildSuccess;
				case EditorNotificationSound.BuildError:
					return _userBuildErrorSound.Clip != null ? _userBuildErrorSound.Clip : _buildError;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}

	