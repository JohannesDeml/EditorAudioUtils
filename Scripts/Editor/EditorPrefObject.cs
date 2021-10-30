// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditorPrefObject.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace JD.EditorAudioUtils
{
	[Serializable]
	public class EditorPrefObject<T> where T : UnityEngine.Object
	{
		[SerializeField]
		protected string _editorKey;

		private bool _initialized;
		private T _object;

		public T Object
		{
			get
			{
				if (!_initialized)
				{
					_object = LoadSavedObject();
					_initialized = true;
				}
				return _object;
			}
			set
			{
				if (!_initialized)
				{
					_object = LoadSavedObject();
					_initialized = true;
				}
				
				if (_object == value)
				{
					return;
				}

				_object = value;
				if (_object == null)
				{
					DeleteKey();
					return;
				}
			
				var path = AssetDatabase.GetAssetPath(value);
				var guid = AssetDatabase.GUIDFromAssetPath(path);
				EditorPrefs.SetString(_editorKey, guid.ToString());
			}
		}
		
		public EditorPrefObject(string editorKey)
		{
			_editorKey = editorKey;
		}

		private T LoadSavedObject()
		{
			var storedGuid = EditorPrefs.GetString(_editorKey, null);
			if (storedGuid == null)
			{
				return null;
			}

			var path = AssetDatabase.GUIDToAssetPath(storedGuid);
			if (string.IsNullOrEmpty(path))
			{
				return null;
			}

			return AssetDatabase.LoadAssetAtPath<T>(path);
		}

		public void DeleteKey()
		{
			EditorPrefs.DeleteKey(_editorKey);
		}
	}
}