// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorPrefAudioClip.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace JD.EditorAudioUtils
{
	[Serializable]
	public class EditorPrefAudioClip : EditorPrefObject<AudioClip>
	{
		public AudioClip Clip => Object;
		public EditorPrefAudioClip(string editorKey) : base(editorKey)
		{
		}
	}
}