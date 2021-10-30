// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorPrefAudioClipDrawer.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace JD.EditorAudioUtils
{
	/// <summary>
	/// Draws the EditorPref with hiding its EditorPref key and acting as if it was just a normal object field
	/// </summary>
	[CustomPropertyDrawer(typeof(EditorPrefAudioClip))]
	public class EditorPrefAudioClipDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var audioPrefObject = EditorUtilities.GetFieldOrPropertyValue<EditorPrefAudioClip>(
				property.name, property.serializedObject.targetObject);

			if (audioPrefObject == null)
			{
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			AudioClip audioClip = audioPrefObject.Object;
			position.width -= EditorAudioButton.Styles.ButtonWidth + EditorAudioButton.Styles.Padding;

			EditorGUI.BeginChangeCheck();
			audioClip = EditorGUI.ObjectField(position, label, audioClip, typeof(AudioClip), false) as AudioClip;
			if (EditorGUI.EndChangeCheck())
			{
				audioPrefObject.Object = audioClip;
			}

			position.x += position.width + EditorAudioButton.Styles.Padding;
			position.width = EditorAudioButton.Styles.ButtonWidth;

			EditorGUI.BeginDisabledGroup(audioClip == null);
			EditorAudioButton.DrawAudioButton(position, audioClip);
			EditorGUI.EndDisabledGroup();
		}
	}
}