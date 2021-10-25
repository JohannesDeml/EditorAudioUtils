// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PreviewAudioClipAttribute.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace JD.EditorAudioUtils
{
	/// <summary>
	/// Add a preview button to preview and stop an audio clip
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
	public class PreviewAudioClipAttribute : PropertyAttribute
	{
		public PreviewAudioClipAttribute()
		{
		}
	}
}