using DataLayer.Context;
using DataLayer.Contract;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repozitory
{
	public class CategoryRepozitory : GenericRepozitory<Category> ,ICategoryRepozitory
	{
		private readonly TrainDbContext context;

		public CategoryRepozitory(TrainDbContext context):base(context)
        {
			this.context = context;
		}

		public List<Category> FindActive()
		{
			return context.Categories.Where(c=>c.Isdelete==false).ToList();
		}

		public Category FindAllById(int id)
		{
			return context.Categories.Find(id);
		}

		public Category FindbyId(int id)
		{
			return context.Categories.FirstOrDefault(c => c.MyCategoryID == id);
		}

		public string FindName(int idcategory)
		{
			var rez=context.Categories.FirstOrDefault(c => c.MyCategoryID == idcategory);
			return rez.description;
		}

		public bool isExist(string name)
		{
			return context.Categories.Any(c => c.description == name);
		}

		public List<Category> Oreder()
		{
			return context.Categories.OrderByDescending(c=>c.Createdate).ToList();
		}

	
	}
}
