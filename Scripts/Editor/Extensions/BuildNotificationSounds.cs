// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildNotificationSounds.cs">
//   Copyright (c) 2024 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace JD.EditorAudioUtils.Extensions
{
	/// <summary>
	/// Plays notification sounds when the build has finished
	/// </summary>
	public class BuildNotificationSounds : IPostprocessBuildWithReport
	{
		public int callbackOrder => 100_000;
	
		public void OnPostprocessBuild(BuildReport report)
		{
			if (report.summary.result != BuildResult.Failed)
			{
				EditorAudioUtility.PlayNotificationSound(EditorNotificationSound.BuildSuccess);
			}
			else
			{
				EditorAudioUtility.PlayNotificationSound(EditorNotificationSound.BuildError);
			}
		}
	}
}