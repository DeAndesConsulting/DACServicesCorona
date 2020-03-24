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
		where R : class, new()
		where E : class, new()
	{
		private R repository;

		public ServiceBaseBusiness()
		{
			repository = new R();
		}

		public object Create(E entity)
		{
			//ServicePacienteRepository repo = new ServicePacienteRepository();
			//repo.Create()
			//return servicePreguntaRepository.Create(pregunta);
			MethodInfo methodInfo = typeof(R).GetMethod("Create");
			MethodInfo methodInfoGeneric = methodInfo.MakeGenericMethod(typeof(R));
			return methodInfoGeneric.Invoke(this, new[] { entity });
		}

		public object Read(Func<E, bool> predicado)
		{
			//return servicePreguntaRepository.Read(predicado);
			MethodInfo methodInfo = typeof(R).GetMethod("Read");
			MethodInfo methodInfoGeneric = methodInfo.MakeGenericMethod(typeof(R));
			return methodInfoGeneric.Invoke(this, new[] { predicado });
		}

		public object Read()
		{
			//return servicePreguntaRepository.Read();
			MethodInfo methodInfo = typeof(R).GetMethod("Read");
			MethodInfo methodInfoGeneric = methodInfo.MakeGenericMethod(typeof(R));
			return methodInfoGeneric.Invoke(this, null);

		}

		public object Update(E entity)
		{
			//return servicePreguntaRepository.Update(pregunta);
			MethodInfo methodInfo = typeof(R).GetMethod("Update");
			MethodInfo methodInfoGeneric = methodInfo.MakeGenericMethod(typeof(R));
			return methodInfoGeneric.Invoke(this, new[] { entity });
		}

		public object Delete(E entity)
		{
			//return servicePreguntaRepository.Delete(pregunta);
			MethodInfo methodInfo = typeof(R).GetMethod("Delete");
			MethodInfo methodInfoGeneric = methodInfo.MakeGenericMethod(typeof(R));
			return methodInfoGeneric.Invoke(this, new[] { entity });
		}
	}
}
