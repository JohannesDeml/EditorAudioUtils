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
		private static class Styles
		{
			public static readonly float ButtonWidth = 20f;
			public static readonly float Padding = 4f;
			
			private static GUIStyle _buttonStyle;
			private static Texture2D _playButtonTexture;
			private static Texture2D _stopButtonTexture;
			public static GUIStyle ButtonStyle
			{
				get
				{
					if (_buttonStyle == null)
					{
						_buttonStyle = new GUIStyle((GUIStyle)"IconButton");
					}
					return _buttonStyle;
				}
			}
			public static Texture2D PlayButtonTexture
			{
				get
				{
					if (_playButtonTexture == null)
					{
						_playButtonTexture = EditorGUIUtility.FindTexture("d_PlayButton");
					}
					return _playButtonTexture;
				}
			}
			public static Texture2D StopButtonTexture
			{
				get
				{
					if (_stopButtonTexture == null)
					{
						_stopButtonTexture = EditorGUIUtility.FindTexture("d_PreMatQuad");
					}
					return _stopButtonTexture;
				}
			}
			
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.ObjectReference)
			{
				EditorGUI.PropertyField(position, property, label);
				return;
			}

			AudioClip audioObject = property.objectReferenceValue as AudioClip;
			position.width -= Styles.ButtonWidth + Styles.Padding;
			EditorGUI.PropertyField(position, property, label);
			position.x += position.width + Styles.Padding;
			position.width = Styles.ButtonWidth;
			
			EditorGUI.BeginDisabledGroup(audioObject == null);
			DrawButton(position, audioObject);
			EditorGUI.EndDisabledGroup();
		}

		private void DrawButton(Rect position, AudioClip audioObject)
		{
			if (audioObject == null)
			{
				DrawPlayButton(position, audioObject);
				return;
			}

			bool currentlyPlaying = EditorAudioUtility.LastPlayedPreviewClip == audioObject
			                        && EditorAudioUtility.IsPreviewClipPlaying(audioObject);
			if (currentlyPlaying)
			{
				DrawStopButton(position, audioObject);
			}
			else
			{
				DrawPlayButton(position, audioObject);
			}
		}

		private void DrawPlayButton(Rect position, AudioClip audioObject)
		{
			if (GUI.Button(position, Styles.PlayButtonTexture, Styles.ButtonStyle))
			{
				EditorAudioUtility.PlayPreviewClip(audioObject);
			}
		}
		
		private void DrawStopButton(Rect position, AudioClip audioObject)
		{
			if (GUI.Button(position, Styles.StopButtonTexture, Styles.ButtonStyle))
			{
				EditorAudioUtility.StopPreviewClip(audioObject);
			}
		}
	}
}