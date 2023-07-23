using System;
using System.Collections.Generic;
using UnityEngine;

namespace MetaRTS_Legacy
{
	public class ObjectsPool<T>
	{
		public static ObjectsPool<U> CreateObjectsPool<U>() where U : new()
		{
			return new ObjectsPool<U>(() => new U());
		}

		public static ObjectsPool<U> CreateGameObjectsPool<U>(U prefab) where U : Component
		{
			return new ObjectsPool<U>(() => GameObject.Instantiate(prefab));
		}

		public static ObjectsPool<U> CreateGameObjectsPool<U>(U prefab, Transform parent) where U : Component
		{
			return new ObjectsPool<U>(() => GameObject.Instantiate(prefab, parent));
		}

		private Queue<T>  _pool;
		public  Func<T>   Creator;
		public  Action<T> LockHandler;
		public  Action<T> FreeHandler;
		public  Action<T> Disposer;
		public  Action    FinalDispose;

		public ObjectsPool(
				Func<T>   itemCreator  = null,
				Action<T> handleLock   = null,
				Action<T> handleFree   = null,
				Action<T> itemDisposer = null,
				Action    dispose      = null
			)
		{
			_pool        = new Queue<T>();
			Creator      = itemCreator;
			LockHandler  = handleLock;
			FreeHandler  = handleFree;
			Disposer     = itemDisposer;
			FinalDispose = dispose;
		}

		public void Dispose()
		{
			if (Disposer != null)
				foreach (var item in _pool)
					Disposer(item);
			_pool.Clear();
			
			FinalDispose?.Invoke();
		}

		public T Lock()
		{
			if (_pool.Count > 0)
			{
				var item = _pool.Dequeue();
				LockHandler?.Invoke(item);
				return item;
			}
			if (Creator == null)
				return default;
			return Creator();
		}

		public void Free(T item)
		{
			FreeHandler?.Invoke(item);
			_pool.Enqueue(item);
		}
	}
}