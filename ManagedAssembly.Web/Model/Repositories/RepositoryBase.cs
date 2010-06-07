using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Reflection;

using SubSonic;
using SubSonic.Repository;
using SubSonic.Schema;

namespace ManagedAssembly.Data
{
	public class RepositoryBase<T> : SubSonicRepository<T>
		where T : class, new()
	{
		public RepositoryBase() : base(new ManagedAssemblyDB())
		{

		}

		protected ManagedAssemblyDB DB {
			get {
				return new ManagedAssemblyDB();
			}
		}

		protected List<T> GetList(IQueryable<T> query) {
			return query.ToList();
		}

		protected List<T> GetList(IOrderedQueryable<T> query) {
			return query.ToList();
		}

		protected List<T> GetList(IQueryable<T> query, int pageNo, int pageSize) {
			return query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
		}

		protected List<T> GetList(IOrderedQueryable<T> query, int pageNo, int pageSize) {
			return query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
		}

		protected T GetFirst(IQueryable<T> q) {
			return q.FirstOrDefault();
		}

		protected T GetFirst(IOrderedQueryable<T> q) {
			return q.FirstOrDefault();
		}

		protected List<T> GetList(StoredProcedure sproc) {
			return sproc.ExecuteTypedList<T>();
		}

		protected T GetFirst(StoredProcedure sproc) {
			List<T> coll = sproc.ExecuteTypedList<T>();
			return coll.FirstOrDefault();
		}
	}
}