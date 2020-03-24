using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Repositories.Service
{
	public class ServiceBaseRepository<E> where E : class, new()
	{
		private E result;
		private List<E> listaResult = null;

		public object Create(E entity)
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					contexto.Configuration.LazyLoadingEnabled = false;
					result = contexto.Set<E>().Add(entity);
					contexto.SaveChanges();
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object Read(Func<E, bool> predicado)
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					contexto.Configuration.LazyLoadingEnabled = false;
					listaResult = contexto.Set<E>().Where(predicado).ToList<E>();
				}
				return listaResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object Read()
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					contexto.Configuration.LazyLoadingEnabled = false;
					listaResult = contexto.Set<E>().ToList<E>();
				}
				return listaResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object Update(E entity)
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					contexto.Entry(entity).State = System.Data.Entity.EntityState.Modified;
					contexto.SaveChanges();
					return entity;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object Delete(E entity)
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					contexto.Set<E>().Attach(entity);
					result = contexto.Set<E>().Remove(entity);
					contexto.SaveChanges();
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
