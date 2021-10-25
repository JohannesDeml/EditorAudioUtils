// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AudioUtilMethodWrapper.cs">
//   Copyright (c) 2021 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

namespace JD.EditorAudioUtils
{
	/// <summary>
	/// Little wrapper to make it easier to define the methods in the audio utility class
	/// </summary>
	public class AudioUtilMethodWrapper
	{
		private readonly MethodInfo _method;
		private readonly object[] _parameters;

		public AudioUtilMethodWrapper(Type audioUtilClass, string methodName, Type[] callParameterTypes)
		{
			_method = audioUtilClass.GetMethod(
				methodName,
				BindingFlags.Static | BindingFlags.Public,
				null,
				callParameterTypes,
				null
			);

			_parameters = callParameterTypes.Length > 0 ? new object[callParameterTypes.Length] : Array.Empty<object>();
		}

		public object Invoke()
		{
			if (_parameters.Length != 0)
			{
				throw new Exception($"Called with the wrong number of arguments. Expected {_parameters.Length}, Actual: 0");
			}

			return _method.Invoke(null, _parameters);
		}
		
		public object Invoke(object arg0)
		{
			if (_parameters.Length != 1)
			{
				throw new Exception($"Called with the wrong number of arguments. Expected {_parameters.Length}, Actual: 1");
			}

			_parameters[0] = arg0;
			return _method.Invoke(null, _parameters);
		}
		
		public object Invoke(object arg0, object arg1)
		{
			if (_parameters.Length != 2)
			{
				throw new Exception($"Called with the wrong number of arguments. Expected {_parameters.Length}, Actual: 2");
			}

			_parameters[0] = arg0;
			_parameters[1] = arg1;
			return _method.Invoke(null, _parameters);
		}
		
		public object Invoke(object arg0, object arg1, object arg2)
		{
			if (_parameters.Length != 3)
			{
				throw new Exception($"Called with the wrong number of arguments. Expected {_parameters.Length}, Actual: 3");
			}

			_parameters[0] = arg0;
			_parameters[1] = arg1;
			_parameters[2] = arg2;
			return _method.Invoke(null, _parameters);
		}
	}
}