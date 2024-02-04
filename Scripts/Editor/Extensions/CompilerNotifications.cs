// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompilerNotifications.cs">
//   Copyright (c) 2024 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;

namespace JD.EditorAudioUtils.Extensions
{
	/// <summary>
	/// Plays notification sounds when the compilation has finished
	/// If you set the sounds to null in the settings, no sound will be played (might be preferred for successful compilations)
	/// </summary>
	public static class CompilerNotificationSounds
	{
		private static bool hasCompileErrors = false;
		
		[InitializeOnLoadMethod]
		private static void Init()
		{
			CompilationPipeline.assemblyCompilationFinished -= ProcessAssemblyCompileFinish;
			CompilationPipeline.assemblyCompilationFinished += ProcessAssemblyCompileFinish;
			
			CompilationPipeline.compilationStarted -= ProcessCompileStart;
			CompilationPipeline.compilationStarted += ProcessCompileStart;
			
			CompilationPipeline.compilationFinished -= ProcessCompileFinish;
			CompilationPipeline.compilationFinished += ProcessCompileFinish;
		}

		private static void ProcessAssemblyCompileFinish(string s, CompilerMessage[] compilerMessages)
		{
			hasCompileErrors |= compilerMessages.Any(m => m.type == CompilerMessageType.Error);
			
		}
		
		private static void ProcessCompileStart(object obj)
		{
			hasCompileErrors = false;
		}
		
		private static void ProcessCompileFinish(object obj)
		{
			if (!hasCompileErrors)
			{
				EditorAudioUtility.PlayNotificationSound(EditorNotificationSound.CompileSuccess);
			}
			else
			{
				EditorAudioUtility.PlayNotificationSound(EditorNotificationSound.CompileError);
			}
		}
	}
}