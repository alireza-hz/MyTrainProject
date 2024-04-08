using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Contract
{
	public interface ICategoryRepozitory : IGenericRepozitory<Category>
	{
		List<Category> FindActive();

		string FindName (int idcategory);

		Category FindbyId(int id);

		List<Category> Oreder();

		Category FindAllById(int id);

		bool isExist(string name);
	}
}
