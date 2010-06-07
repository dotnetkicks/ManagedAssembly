using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagedAssembly.Web
{
	public interface ICacheProvider
	{
		void Store(string key, object data);
		void Store(string key, object data, DateTime expires);
		void Destroy(string key);
		T Get<T>(string key);
	}

	public class RequestProvider : ICacheProvider
	{
		public RequestProvider()
		{
			if (HttpContext.Current == null)
			{
				throw new ApplicationException("HttpContext.Current is not available.");
			}
		}

		public void Store(string key, object data)
		{
			if (HttpContext.Current.Items.Contains(key))
				HttpContext.Current.Items[key] = data;
			else
				HttpContext.Current.Items.Add(key, data);
		}

		public void Store(string key, object data, DateTime expires) {
			throw new NotImplementedException("Provider does not support caching items longer than length of request");
		}

		public void Destroy(string key)
		{
			HttpContext.Current.Items.Remove(key);
		}

		public T Get<T>(string key)
		{
			T item = (T)HttpContext.Current.Items[key];
			if (item == null)
			{
				item = default(T);
			}
			return item;
		}
	}

	public class ShortTermProvider : ICacheProvider
	{
		public ShortTermProvider()
		{
			if (HttpRuntime.Cache == null)
			{
				throw new ApplicationException("HttpRuntime.Cache is not available.");
			}
		}

		public void Store(string key, object data)
		{
			HttpRuntime.Cache.Insert(key, data);
		}

		public void Store(string key, object data, DateTime expires) {
			HttpRuntime.Cache.Insert(key, data, null, expires, System.Web.Caching.Cache.NoSlidingExpiration);
		}

		public void Destroy(string key)
		{
			HttpRuntime.Cache.Remove(key);
		}

		public T Get<T>(string key)
		{
			if (HttpRuntime.Cache[key] == null)
				return default(T);

			T item = (T)HttpRuntime.Cache[key];
			if (item == null)
			{
				item = default(T);
			}
			return item;
		}
	}

	public class CacheManager
	{
		protected ICacheProvider _provider;
		public CacheManager(ICacheProvider provider)
		{
			_provider = provider;
		}

		public void Store(string key, object data) {
			_provider.Store(key, data);
		}

		public void Store(string key, object data, DateTime expires) {
			_provider.Store(key, data, expires);
		}

		public void Destroy(string key)
		{
			_provider.Destroy(key);
		}

		public T Get<T>(string key)
		{
			return _provider.Get<T>(key);
		}
	}
}
