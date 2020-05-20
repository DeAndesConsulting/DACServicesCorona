using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DACServices.Interfaces
{
	public interface IServiceBaseRepository<E> where E: class, new()
	{
		E Create(E entity);
		object Read();
		object Read(Func<E, bool> predicate);
		E Update(E entity);
		object Delete(E entity);
	}
}
