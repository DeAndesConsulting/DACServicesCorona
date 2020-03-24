using DACServices.Entities;
using DACServices.Repositories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	public class ServicePreguntaBusiness
	{
		private ServicePreguntaRepository servicePreguntaRepository;

		public ServicePreguntaBusiness()
		{
			servicePreguntaRepository = new ServicePreguntaRepository();
		}

		public object Create(tbPregunta pregunta)
		{
			return servicePreguntaRepository.Create(pregunta);
		}

		public object Read(Func<tbPregunta, bool> predicado)
		{
			return servicePreguntaRepository.Read(predicado);
		}

		public object Read()
		{
			return servicePreguntaRepository.Read();
		}

		public object Update(tbPregunta pregunta)
		{
			return servicePreguntaRepository.Update(pregunta);
		}

		public object Delete(tbPregunta pregunta)
		{
			return servicePreguntaRepository.Delete(pregunta);
		}
	}
}
