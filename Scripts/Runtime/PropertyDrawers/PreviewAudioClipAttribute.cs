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
		/// Add a draggable icon at front and a goto arrow at the back of a ScriptableObject reference.
		/// </summary>
		public PreviewAudioClipAttribute()
		{
		}
	}
}