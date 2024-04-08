using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Contract
{
	public interface ILevelRepozitory : IGenericRepozitory<Level>
	{

		List<string> findlevel();
	}
}
