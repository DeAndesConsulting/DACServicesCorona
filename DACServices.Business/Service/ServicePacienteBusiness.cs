using DACServices.Entities;
using DACServices.Repositories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Business.Service
{
	//public class ServicePacienteBusiness : ServiceBaseBusiness<ServicePacienteRepository, tbPaciente>
	public class ServicePacienteBusiness
	{
		private ServicePacienteRepository servicePacienteRepository;

		public ServicePacienteBusiness()
		{
			servicePacienteRepository = new ServicePacienteRepository();
		}

		public object Create(tbPaciente paciente)
		{
			return servicePacienteRepository.Create(paciente);
		}

		public object Read(Func<tbPaciente, bool> predicado)
		{
			return servicePacienteRepository.Read(predicado);
		}

		public object Read()
		{
			return servicePacienteRepository.Read();
		}

		public object Update(tbPaciente paciente)
		{
			return servicePacienteRepository.Update(paciente);
		}

		public object Delete(tbPaciente paciente)
		{
			return servicePacienteRepository.Delete(paciente);
		}
	}
}
