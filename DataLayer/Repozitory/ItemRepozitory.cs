using DataLayer.Context;
using DataLayer.Contract;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Convert;
using Application.ViewModel;

namespace DataLayer.Repozitory
{
	public class ItemRepozitory :GenericRepozitory<Item> , IitemRepozitory
	{
		private readonly TrainDbContext context;

		public ItemRepozitory(TrainDbContext context):base(context)
        {
			this.context = context;
		}

		public List<Item> finditme(int id)
		{
			return  context.Items.Where(c=>c.MYUserID ==id).ToList();
						
		}

		public List<int> findIDProduct(int id)
		{
		  return context.Items.Where(c => c.MYUserID == id & c.IsConfirm ==false ).Select(c => c.MyProductId).ToList();
		}

		public bool IsExsistCourse(int userid, int productid)
		{
			return context.Items.Any(c => c.MYUserID == userid & c.MyProductId == productid);
		}

		public Item findItemForDelete(int userid, int productid)
		{
			return context.Items.FirstOrDefault(c => c.MYUserID == userid & c.MyProductId == productid);
		}

        public List<Item> finditeminfo(int id, List<int> ints)
        {
            return context.Items.Include(c=>c.MyProduct).ThenInclude(c=>c.Level).Where(c=>c.MYUserID == id & ints.Contains(c.MyProductId)).ToList();
        }

        public Item find(int itemid)
        {
			return context.Items.FirstOrDefault(c=>c.ItemId == itemid);
        }

        public List<int> findIDProductAdmin(int userid)
        {
           return context.Items.Where(c => c.MYUserID == userid ).Select(c => c.MyProductId).ToList();
        }

        public List<Item> FindInfoSale()
        {
            return context.Items.Include(c=>c.MyUser).Include(c=>c.MyProduct).ThenInclude(c=>c.Level).Where(c=>c.IsConfirm ==true).ToList();
        }

		public List<ItemInfo> FindInfoSaledaily(List<int> itemid)
		{
			return context.Items.Include(c => c.MyProduct).Where(c => itemid.Contains(c.ItemId)).Select(c => new ItemInfo
			{
				ItemId = c.ItemId,
				DateTime = c.CreateDate.Toshamsi(),
			}).ToList();
		}

		public int sumpricetodey(List<int> itemid)
		{
			return context.Items.Include(c => c.MyProduct).Where(c => itemid.Contains(c.ItemId)).Select(i => i.MyProduct.Price).Sum();
		}

		public List<ItemInfo> FindInfoSaleMonthly(List<int> itemid)
		{
			return context.Items.Include(c => c.MyProduct).Where(c => itemid.Contains(c.ItemId)).Select(c => new ItemInfo
			{
				ItemId = c.ItemId,
				DateTime = c.CreateDate.ToshamsiGetMounth(),
			}).ToList();
		}

		public int sumpriceMounth(List<int> itemid)
		{
			return context.Items.Include(c => c.MyProduct).Where(c => itemid.Contains(c.ItemId)).Select(i => i.MyProduct.Price).Sum();
		}


		public int CountAddTocart()
		{
		  return context.Items.Where(c=>c.IsConfirm == true).Count();
		}

		public List<int> countitem()
		{
			return context.Items.Where(i=>i.IsConfirm == true).Select(i=>i.ItemId).ToList();
		}

		public int CountCourceUser(int userid)
		{
			return context.Items.Where(c => c.MYUserID == userid & c.IsConfirm == true).Count();
		}

		public List<Item> finduserofcoure(int userid)
		{
			return context.Items.Include(c=>c.MyProduct).ThenInclude(c=>c.Level).Where(c => c.MYUserID == userid & c.IsConfirm == true).ToList(); 
		}
	}
}
