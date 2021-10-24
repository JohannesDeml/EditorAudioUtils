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

namespace JD.EditorAudioUtils
{
	public static class EditorAudioUtilsProjectRegister
	{
		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider()
		{
			// First parameter is the path in the Settings window.
			// Second parameter is the scope of this setting: it only appears in the Project Settings window.
			var provider = new SettingsProvider("Project/EditorAudioUtils", SettingsScope.Project)
			{
				// By default the last token of the path is used as display name if no label is provided.
				label = "EditorAudioUtils",
				// Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
				guiHandler = (searchContext) =>
				{
					var settings = EditorAudioSettings.Instance;
					var editor = Editor.CreateEditor(settings);
					editor.OnInspectorGUI();
				},

				// Populate the search keywords to enable smart search filtering and label highlighting:
				keywords = new HashSet<string>(new[] { "Editor", "Audio", "Sound", "SFX", "Notifications" })
			};

			return provider;
		}
	}
}