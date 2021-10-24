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
	[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
	public class PreviewAudioClipAttribute : PropertyAttribute
	{
		/// <summary>
		/// Add a preview button to preview an audio clip
		/// </summary>
		public PreviewAudioClipAttribute()
		{
		}
	}
}