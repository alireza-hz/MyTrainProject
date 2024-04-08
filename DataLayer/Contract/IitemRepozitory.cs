using Domain.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModel;
namespace DataLayer.Contract
{
	public interface IitemRepozitory : IGenericRepozitory<Item>
	{
		List<Item> finditme(int id);

		bool IsExsistCourse(int userid , int productid);

		List<int> findIDProduct (int userid);


        Item findItemForDelete(int userid, int productid);

		List<Item> finditeminfo (int id , List<int> ints);

		Item find(int itemid);

        List<int> findIDProductAdmin(int userid);

		List<Item> FindInfoSale();

		List<int> countitem();
		int CountAddTocart();

		int CountCourceUser(int userid);

	
		List<ItemInfo> FindInfoSaleMonthly(List<int> itemid);
		List<ItemInfo> FindInfoSaledaily(List<int> itemid);

		int sumpricetodey(List<int> itemid);
		int sumpriceMounth(List<int> itemid);


		List<Item> finduserofcoure(int userid);
	}
}
