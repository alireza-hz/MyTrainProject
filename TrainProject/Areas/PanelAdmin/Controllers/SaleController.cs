using Application.Convert;
using DataLayer.Contract;
using DataLayer.Repozitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TrainProject.Areas.PanelAdmin.Controllers
{
    [Area("PanelAdmin")]
	[Authorize]
	public class SaleController : Controller
    {
        private readonly IitemRepozitory itemRepozitory;
		

		public SaleController(IitemRepozitory itemRepozitory )
        {
            this.itemRepozitory = itemRepozitory;
			
		}
        public IActionResult Index(int pageid)
        {
            ViewBag.time = DateTime.Now.Toshamsi();
            int take = 10;
            int skip = (pageid - 1) * take;
            var rez = itemRepozitory.FindInfoSale().Skip(skip).Take(take);
            int productCount =rez.Count();
            ViewBag.productCount = productCount;
            ViewBag.PageId = pageid;
            ViewBag.Take = take;
            var pageCount = (productCount / take);
            ViewBag.pageCount = pageCount;
            ViewBag.info = rez;


            var itemids= new List<int>();
            foreach (var Items in rez)
            {
                itemids.Add(Items.ItemId);
            }

            var rez5 = itemRepozitory.FindInfoSaledaily(itemids);		
			var today = DateTime.Now.Toshamsi();
            var id = new List<int>();
            for (int i =0; i < rez5.Count; i++)
            {
                if (rez5[i].DateTime == today)
                {
                    id.Add(rez5[i].ItemId);
                }
            }
			ViewBag.countdaily = id.Count();
			var rez6 = itemRepozitory.sumpricetodey(id);
            ViewBag.priceTodey = rez6;


            var rez7 = itemRepozitory.FindInfoSaleMonthly(itemids);            
			var mounth  = DateTime.Now.ToshamsiGetMounth();
			var month = new List<int>();
			for (int i = 0; i < rez7.Count; i++)
			{
				if (rez7[i].DateTime == mounth)
				{
					month.Add(rez7[i].ItemId);
				}
			}
			ViewBag.countMouncly = month.Count();
            var rez8 = itemRepozitory.sumpriceMounth(month);
            ViewBag.priceMounth = rez8;

            return View();
        }
    }
}
