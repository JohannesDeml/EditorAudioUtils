// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorAudioUtilsProjectRegister.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JD.EditorAudioUtils
{
	public static class EditorAudioUtilsProjectRegister
	{
		private static Editor _settingsEditor;
		
		/// <summary>
		/// Add project settings tab for EditorAudioUtils
		/// </summary>
		/// <returns></returns>
		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider()
		{
			var provider = new SettingsProvider("Project/EditorAudioUtils", SettingsScope.Project)
			{
				label = "EditorAudioUtils",
				guiHandler = (searchContext) =>
				{
					// Draw documentation url
					if (GUILayout.Button("https://github.com/JohannesDeml/EditorAudioUtils", GUI.skin.label))
					{
						Application.OpenURL("https://github.com/JohannesDeml/EditorAudioUtils");
					}
					EditorGUILayout.Space();

					EditorGUILayout.LabelField("Project Settings", EditorStyles.boldLabel);
					EditorNotificationSettings settings = EditorNotificationSettings.Instance;
					using (new EditorGUI.DisabledScope(true))
					{
						EditorGUILayout.ObjectField("Settings", settings, typeof(EditorNotificationSettings), false);
					}

					// Draw project settings
					Editor.CreateCachedEditor(settings, null, ref _settingsEditor);
					_settingsEditor.OnInspectorGUI();

					// Draw user specific settings
					bool soundsEnabled = EditorNotificationSettings.NotificationSoundsEnabled;
					EditorGUI.BeginChangeCheck();
					soundsEnabled = EditorGUILayout.Toggle("Enable Notification Sounds", soundsEnabled);
					if (EditorGUI.EndChangeCheck())
					{
						EditorNotificationSettings.NotificationSoundsEnabled = soundsEnabled;
						// This will only play when notification sounds are enabled
						EditorAudioUtility.PlayNotificationSound(EditorNotificationSound.Success);
					}
				},

				// Populate the search keywords to enable smart search filtering and label highlighting:
				keywords = new HashSet<string>(new[] { "Editor", "Audio", "Sound", "Notifications", 
					"Success Sound", "Warning Sound", "Error Sound", "Info Sound", "Enable Notification Sounds" })
			};

			return provider;
		}
	}
}