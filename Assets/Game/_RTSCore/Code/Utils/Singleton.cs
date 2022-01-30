using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace MetaRTS
{
	public class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		private static T _instance;
		public static T Instance
		{
			get
			{
				if (_instance==null)
					_instance = FindObjectOfType<T>();
				return _instance;
			}
		}

		public virtual void Awake()
		{
			if (_instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				_instance = (T) this;
			}
		}
	}
}