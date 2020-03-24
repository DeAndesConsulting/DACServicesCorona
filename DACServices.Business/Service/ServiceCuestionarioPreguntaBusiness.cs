using DACServices.Entities;
using DACServices.Repositories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	public class ServiceCuestionarioPreguntaBusiness
	{
		private ServiceCuestionarioPreguntaRepository serviceCuestionarioPreguntaRepository;

		public ServiceCuestionarioPreguntaBusiness()
		{
			serviceCuestionarioPreguntaRepository = new ServiceCuestionarioPreguntaRepository();
		}

		public object Create(tbCuestionarioPregunta cuestionarioPregunta)
		{
			return serviceCuestionarioPreguntaRepository.Create(cuestionarioPregunta);
		}

		public object Read(Func<tbCuestionarioPregunta, bool> predicado)
		{
			return serviceCuestionarioPreguntaRepository.Read(predicado);
		}

		public object Read()
		{
			return serviceCuestionarioPreguntaRepository.Read();
		}

		public object Update(tbCuestionarioPregunta cuestionarioPregunta)
		{
			return serviceCuestionarioPreguntaRepository.Update(cuestionarioPregunta);
		}

		public object Delete(tbCuestionarioPregunta cuestionarioPregunta)
		{
			return serviceCuestionarioPreguntaRepository.Delete(cuestionarioPregunta);
		}
	}
}
