using DACServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Repositories.Service
{
	public class ServicePreguntaRepository
	{
		private tbPregunta result;
		private List<tbPregunta> listaResult = null;

		public object Create(tbPregunta pregunta)
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					result = contexto.tbPregunta.Add(pregunta);
					contexto.SaveChanges();
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object Read(Func<tbPregunta, bool> predicado)
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					contexto.Configuration.LazyLoadingEnabled = false;
					listaResult = contexto.tbPregunta.Where(predicado).ToList<tbPregunta>();
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
					listaResult = contexto.tbPregunta.ToList<tbPregunta>();
				}
				return listaResult;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object Update(tbPregunta pregunta)
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					result = contexto.tbPregunta.SingleOrDefault(x => x.pre_id == pregunta.pre_id);

					if (result != null)
					{
						result.pre_pregunta = pregunta.pre_pregunta;
						result.pre_respuesta_positiva = pregunta.pre_respuesta_positiva;
						contexto.SaveChanges();
					}
					else
					{
						throw new Exception("No se encontro el objeto a actualizar");
					}
				}
				return result;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public object Delete(tbPregunta pregunta)
		{
			try
			{
				using (var contexto = new DB_DACSEntities())
				{
					contexto.tbPregunta.Attach(pregunta);
					result = contexto.tbPregunta.Remove(pregunta);
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
