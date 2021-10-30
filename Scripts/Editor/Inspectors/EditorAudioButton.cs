// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorAudioButton.cs">
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
	/// Helper class for drawing an audio preview button, with which you can play or stop an audio clip
	/// </summary>
	public static class EditorAudioButton
	{
		public static class Styles
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
		
		public static void DrawAudioButton(Rect position, AudioClip audioObject)
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

		private static void DrawPlayButton(Rect position, AudioClip audioObject)
		{
			if (GUI.Button(position, Styles.PlayButtonTexture, Styles.ButtonStyle))
			{
				EditorAudioUtility.PlayPreviewClip(audioObject);
			}
		}
		
		private static void DrawStopButton(Rect position, AudioClip audioObject)
		{
			if (GUI.Button(position, Styles.StopButtonTexture, Styles.ButtonStyle))
			{
				EditorAudioUtility.StopPreviewClip(audioObject);
			}
		}
	}
}