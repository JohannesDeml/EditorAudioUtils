// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorUtilities.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using UnityEditor;
using UnityEngine;

namespace JD.EditorAudioUtils
{
	/// <summary>
	/// Utility class for easy asset handling like creating or replacing files in the project
	/// </summary>
	public static class EditorUtilities
	{
		/// <summary>
		/// Get or Create a ScriptableObject in the EditorDefaultResources folder.
		/// </summary>
		/// <typeparam name="T">Type of the scriptable object</typeparam>
		/// <param name="folderPath">Path to the asset relative to the EditorDefaultResources folder. e.g. QualityAssurance/Materials</param>
		/// <param name="fileName">Name of the file including the extension, e.g. MaterialCollector.asset</param>
		/// <param name="searchOutsideResources">Whether the file should be searched in the complete project if no asset is found at the defined location</param>
		/// <returns>The found or created asset</returns>
		public static T FindOrCreateEditorAsset<T>(string folderPath, string fileName, bool searchOutsideResources) where T : ScriptableObject
		{
			if (folderPath == null)
			{
				folderPath = string.Empty;
			}

			string assetDefaultPath = Path.Combine(folderPath, fileName);
			var asset = EditorGUIUtility.Load(assetDefaultPath) as T;
			if (asset == null && searchOutsideResources)
			{
				asset = LoadAssetOutsideDefaultFolder<T>(assetDefaultPath);
			}
			
			if (asset == null)
			{
				asset = ScriptableObject.CreateInstance<T>();
				var assetRelativeFolderPath = "Assets/Editor Default Resources/" + folderPath;
				// Create folders if not not existent
				Directory.CreateDirectory(Path.GetFullPath(assetRelativeFolderPath));
				var assetRelativeFilePath = Path.Combine(assetRelativeFolderPath, fileName);
				AssetDatabase.CreateAsset(asset, assetRelativeFilePath);
			}

			return asset;
		}

		private static T LoadAssetOutsideDefaultFolder<T>(string assetDefaultPath) where T : ScriptableObject
		{
			// See if the overwritten path was already cached and has a valid value
			string overwrittenPath = EditorPrefs.GetString($"SingletonPathOverwrite.{assetDefaultPath}", null);
			if (overwrittenPath != null)
			{
				T asset = AssetDatabase.LoadAssetAtPath<T>(overwrittenPath);
				if (asset != null)
				{
					return asset;
				}
			}

			// Search through the whole project
			var guids = AssetDatabase.FindAssets($"t:{typeof(T).FullName}");
			if (guids.Length == 0)
			{
				return null;
			}
			
			if (guids.Length > 1)
			{
				Debug.LogWarning($"More than one Asset of the type {typeof(T).FullName} exists:");
				for (int i = 0; i < guids.Length; i++)
				{
					var path = AssetDatabase.GUIDToAssetPath(guids[i]);
					var assetAtPath = AssetDatabase.LoadAssetAtPath<T>(path);
					Debug.Log(path, assetAtPath);
				}
			}

			var pathToFirstAsset = AssetDatabase.GUIDToAssetPath(guids[0]);
			// Save overwritten path
			EditorPrefs.SetString($"SingletonPathOverwrite.{assetDefaultPath}", pathToFirstAsset);
			return AssetDatabase.LoadAssetAtPath<T>(pathToFirstAsset);
		}
	}
}