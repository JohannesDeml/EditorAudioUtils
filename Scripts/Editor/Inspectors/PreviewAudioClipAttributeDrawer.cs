// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreviewAudioClipAttributeDrawer.cs">
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
	[CustomPropertyDrawer(typeof(PreviewAudioClipAttribute))]
	public class PreviewAudioClipAttributeDrawer : PropertyDrawer
	{
		private const float buttonWidth = 20f;
		private const float padding = 4f;
		private GUIStyle folderButtonStyle;
		private Texture2D folderButtonTexture = null;
		
		public PreviewAudioClipAttributeDrawer()
		{
			folderButtonStyle = (GUIStyle)"IconButton";
		}
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
			{
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			if (folderButtonTexture == null)
			{
				folderButtonTexture = EditorGUIUtility.FindTexture("d_PlayButton");
			}

			AudioClip audioObject = property.objectReferenceValue as AudioClip;
			position.width -= buttonWidth + padding;
			EditorGUI.PropertyField(position, property, label);
			position.x += position.width + padding;
			position.width = buttonWidth;
			
			EditorGUI.BeginDisabledGroup(audioObject == null);
			if (GUI.Button(position, folderButtonTexture, folderButtonStyle))
			{
				EditorAudioUtility.PlayAudioClip(audioObject);
			}
			EditorGUI.EndDisabledGroup();
		}
	}
}