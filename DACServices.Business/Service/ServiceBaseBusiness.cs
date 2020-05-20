using DACServices.Interfaces;
using DACServices.Repositories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	public class ServiceBaseBusiness<R, E>
		where R : IServiceBaseRepository<E>, new()
		where E : class, new()
	{
		private R repository;

		public ServiceBaseBusiness()
		{
			repository = new R();
		}

		public object Create(E entity)
		{
			return repository.Create(entity);
		}

		public object Read(Func<E, bool> predicado)
		{
			return repository.Read(predicado);
		}

		public object Read()
		{
			return repository.Read();
		}

		public object Update(E entity)
		{
			return repository.Update(entity);
		}

		public object Delete(E entity)
		{
			return repository.Delete(entity);
		}
	}
}
