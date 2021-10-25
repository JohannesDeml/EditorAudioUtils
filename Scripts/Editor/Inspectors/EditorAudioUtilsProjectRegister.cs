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
					
					// Draw project settings
					EditorGUILayout.LabelField("Project Settings", EditorStyles.boldLabel);
					var settings = EditorNotificationSettings.Instance;
					var editor = Editor.CreateEditor(settings);
					editor.OnInspectorGUI();
					EditorGUILayout.Space();
					
					// Draw user specific settings
					bool soundsEnabled = EditorNotificationSettings.NotificationSoundsEnabled;
					EditorGUI.BeginChangeCheck();
					EditorGUILayout.LabelField("User Settings", EditorStyles.boldLabel);
					soundsEnabled = EditorGUILayout.Toggle("Enable notification sounds", soundsEnabled);
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