using DACServices.Entities;
using DACServices.Repositories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	public class ServiceCuestionarioBusiness
	{
		private ServiceCuestionarioRepository serviceCuestionarioRepository;

		public ServiceCuestionarioBusiness()
		{
			serviceCuestionarioRepository = new ServiceCuestionarioRepository();
		}

		public object Create(tbCuestionario cuestionario)
		{
			return serviceCuestionarioRepository.Create(cuestionario);
		}

		public object Read(Func<tbCuestionario, bool> predicado)
		{
			return serviceCuestionarioRepository.Read(predicado);
		}

		public object Read()
		{
			return serviceCuestionarioRepository.Read();
		}

		public object Update(tbCuestionario cuestionario)
		{
			return serviceCuestionarioRepository.Update(cuestionario);
		}

		public object Delete(tbCuestionario cuestionario)
		{
			return serviceCuestionarioRepository.Delete(cuestionario);
		}
	}
}
