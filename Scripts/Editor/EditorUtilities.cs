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
using System.Reflection;
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
			T asset = null;
			
			// Load from direct path
			if (LoadAssetAtPath(assetDefaultPath, ref asset))
			{
				return asset;
			}
			
			// Load from saved editor pref if exists
			if (LoadFromEditorPrefsGuid(assetDefaultPath, ref asset))
			{
				return asset;
			}
			
			// Search in the project for the asset type
			if (searchOutsideResources && LoadAssetOutsideDefaultFolder(assetDefaultPath, ref asset))
			{
				return asset;
			}
			
			// Create a new asset, since none was found
			asset = ScriptableObject.CreateInstance<T>();
			var assetRelativeFolderPath = "Assets/Editor Default Resources/" + folderPath;
			// Create folders if not not existent
			Directory.CreateDirectory(Path.GetFullPath(assetRelativeFolderPath));
			var assetRelativeFilePath = Path.Combine(assetRelativeFolderPath, fileName);
			AssetDatabase.CreateAsset(asset, assetRelativeFilePath);
				
			string guid = AssetDatabase.AssetPathToGUID(assetRelativeFilePath);
			EditorPrefs.SetString($"EditorSingletonGUID.{assetDefaultPath}", guid);
			return asset;
		}

		private static bool LoadAssetAtPath<T>(string assetDefaultPath, ref T asset) where T : ScriptableObject
		{
			asset = EditorGUIUtility.Load(assetDefaultPath) as T;
			return asset != null;
		}

		private static bool LoadFromEditorPrefsGuid<T>(string assetDefaultPath, ref T asset) where T : ScriptableObject
		{
			string guid = EditorPrefs.GetString($"EditorSingletonGUID.{assetDefaultPath}", null);
			if (guid == null)
			{
				return false;
			}

			var assetPath = AssetDatabase.GUIDToAssetPath(guid);
			if (string.IsNullOrEmpty(assetPath))
			{
				return false;
			}

			asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
			return asset != null;
		}
		
		private static bool LoadAssetOutsideDefaultFolder<T>(string assetDefaultPath, ref T asset) where T : ScriptableObject
		{
			var guids = AssetDatabase.FindAssets($"t:{typeof(T).FullName}");
			if (guids.Length == 0)
			{
				return false;
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

			string foundGuid = guids[0];
			// Save found guid
			EditorPrefs.SetString($"EditorSingletonGUID.{assetDefaultPath}", foundGuid);
			asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(foundGuid));
			return true;
		}
		
		public static T GetFieldOrPropertyValue<T>(string fieldName, object obj,
			BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
		{
			FieldInfo field = obj.GetType().GetField(fieldName, bindings);
			if (field != null) return (T)field.GetValue(obj);

			PropertyInfo property = obj.GetType().GetProperty(fieldName, bindings);
			if (property != null) return (T)property.GetValue(obj, null);

			return default(T);
		}
	}
}