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
	public class ProductRepozitory : GenericRepozitory<MyProduct> , IProductRpozitory
	{
		private readonly TrainDbContext context;

		public ProductRepozitory(TrainDbContext context  ) :base(context) {
			this.context = context;
		}

		public int CountPruductById(int id)
		{
			return context.MyProducts.Count(p=> p.MyCategoryID == id & p.Isdelete==false );
		}

	

		public List<MyProduct> FindActie()
		{
			 return context.MyProducts.Include(c => c.Level).Where(p => p.Isdelete == false).ToList();
		}

		public List<MyProduct> FindActieWithWehere(string name)
		{
			return context.MyProducts.Include(c=>c.Level).Include(c=>c.Category).Where(p=> p.Category.description == name & p.Isdelete == false).ToList();
		}

		public List<MyProduct> FindActiveCourceWithcaregory(int categoryId)
		{
			return context.MyProducts.Include(c=>c.Level).Where(c=>c.Isdelete ==false & c.MyCategoryID ==categoryId).ToList();
		}

		
		public List<MyProduct> FindDeleted()
		{
			return context.MyProducts.Include(c => c.Level).Where(p => p.Isdelete == true).ToList();
		}

		public int FindFinalPrice(List<int> ints)
		{			 
			return context.MyProducts.Where(c => ints.Contains(c.MyProductId)).Select(c => c.Price).Sum();
		}

		public List<MyProduct> findlevelpruduct(int id)
		{
			return context.MyProducts.Include(c=>c.Level).Where(c=>c.MyCategoryID == id).OrderByDescending(c=>c.MyCategoryID).ToList();
		}

		public List<MyProduct> findlevelpruductNOWeher()
		{
			return context.MyProducts.Include(c => c.Level).OrderByDescending(c => c.MyCategoryID).ToList();
		}

		

		public MyProduct FindProduct(int id)
		{
			return context.MyProducts.Include(c=>c.Category).Include(c=>c.Level).FirstOrDefault(c => c.MyProductId == id);
		}

		public MyProduct ProductFindById(int id)
		{

			return context.MyProducts.Find(id);
		  
		}

        public List<MyProduct> Returnlastcource()
        {
			return context.MyProducts.Include(c => c.Level).Where(c => c.Isdelete == false).OrderByDescending(c=>c.Createdate).Take(3).ToList();
        }

		public List<MyProduct> ReturnListproduct(List<int> list)
		{
			return  context.MyProducts.Include(c=>c.Level).Where(c=> list.Contains(c.MyProductId)).ToList();
		}

		public List<MyProduct> ReturnSimilatorCource(int id ,int categotyid)
		{
			return context.MyProducts.Include(c=>c.Level).Include(c=>c.Category).Where(c=>c.MyProductId != id & c.MyCategoryID == categotyid).Take(5).ToList();
		}

		public MyProduct search(string input)
		{
			return context.MyProducts.FirstOrDefault(c => c.ProductName.Contains(input) && c.Isdelete == false);
		}
	}
}
