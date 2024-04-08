using Application.Convert;
using Application.ViewModel;
using DataLayer.Context;
using DataLayer.Contract;
using DataLayer.Repozitory;
using Domain.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace TrainProject.Areas.PanelAdmin.Controllers
{
    [Area("PanelAdmin")]
	[Authorize]
	public class HomeController : Controller
    {
        
        private readonly IProductRpozitory _productRpozitory;
		private readonly ICategoryRepozitory categoryRepozitory;
		private readonly ILevelRepozitory levelRepozitory;
		private readonly IUserRepozirory userRepozirory;
		private readonly IitemRepozitory itemRepozitory;
		private readonly IVisitRepozitory visitRepozitory;

		public HomeController( IProductRpozitory productRpozitory , ICategoryRepozitory categoryRepozitory , ILevelRepozitory levelRepozitory , IUserRepozirory userRepozirory , IitemRepozitory itemRepozitory , IVisitRepozitory visitRepozitory)
        {
            
            _productRpozitory = productRpozitory;
			this.categoryRepozitory = categoryRepozitory;
			this.levelRepozitory = levelRepozitory;
			this.userRepozirory = userRepozirory;
			this.itemRepozitory = itemRepozitory;
			this.visitRepozitory = visitRepozitory;
		}

        public IActionResult Index()
		{
            var rez = _productRpozitory.FindActie();
            ViewBag.countAll= rez.Count;
            var rez1 = userRepozirory.CountuserActive();
            ViewBag.Countuser = rez1;
            var rez3 = itemRepozitory.CountAddTocart();
            ViewBag.CountAddtocart = rez3;
            var rez4 = itemRepozitory.countitem();
			var rez5 = itemRepozitory.FindInfoSaleMonthly(rez4);
			var mounth = DateTime.Now.ToshamsiGetMounth();
			var month = new List<int>();
			for (int i = 0; i < rez5.Count; i++)
			{
				if (rez5[i].DateTime == mounth)
				{
					month.Add(rez5[i].ItemId);
				}
			}
			
			var rez6 = itemRepozitory.sumpriceMounth(month);
			ViewBag.priceMounth = rez6;


			var currentyaer = DateTime.Now.ToshamsiGetDetyear();
			var countmounth = new List<string>();
			for (int i = 1; i <= 12; i++)
			{
				countmounth.Add(currentyaer + "/" + i.ToString("00"));
			}
			var priceofmounth = new List<List<int>>();
			
			for (int j = 0; j <= 11; j++)
			{
				var mounthPrices = new List<int>();
				for (int i = 0; i < rez5.Count; i++)
				{
					if (rez5[i].DateTime == countmounth[j])
					{
						mounthPrices.Add(rez5[i].ItemId);
					}
				}
				priceofmounth.Add(mounthPrices);
			}
			var sumOfPrices = new List<int>();
			foreach (var row in priceofmounth)
			{
				int sum = itemRepozitory.sumpriceMounth(row);
				sumOfPrices.Add(sum); 
			}
			int[] myData = sumOfPrices.ToArray();
			ViewBag.MyData = myData;





			var visit = visitRepozitory.CountVisit();
			var rez7 = visitRepozitory.listvisitdaily(visit);
			var today = DateTime.Now.Toshamsi();
			var IPAddress = new List<string>();
			for (int i = 0; i < rez7.Count; i++)
			{
				if (rez7[i].timevisit == today)
				{
					IPAddress.Add(rez7[i].IPAddress);
				}
			}
			ViewBag.countvisitdaily = IPAddress.Count();

			
			
			var rez8 = visitRepozitory.listvisitMonthly(visit);
			var mounth1 = DateTime.Now.ToshamsiGetMounth();
			var IPAddress1 = new List<string>();
			for (int i = 0; i < rez8.Count; i++)
			{
				if (rez8[i].timevisit == mounth1)
				{
					IPAddress1.Add(rez8[i].IPAddress);
				}
			}
			ViewBag.countvisitmounthly = IPAddress1.Count();

			var rez10 = visitRepozitory.counAll();
			ViewBag.countvisitAll = rez10;


			
			var rez11 = visitRepozitory.dateTimes();
		
			var listtime = new List<DateTime>();
			for (int i = 0; i < rez11.Count; i++)
			{
				listtime.Add(rez11[i].AddMinutes(2));
			}
		    int countonline = 0;
			for (int i = 0; i < rez11.Count; i++)
			{
				if (DateTime.Now <= listtime[i])
				{
					countonline += 1;
				}
			}
			ViewBag.countonline = countonline;



			return View();
        }

        public IActionResult AdminCourceList(int pageid = 1) 
        {
			int take = 5;
			int skip = (pageid - 1) * take;
			var res =_productRpozitory.findlevelpruductNOWeher().Skip(skip).Take(take).ToList();
            
            int productCount = _productRpozitory.GetAll().Count();
			ViewBag.productCount = productCount;
			ViewBag.PageId = pageid;
			ViewBag.Take = take;
			var pageCount = (productCount / take);
			ViewBag.pageCount = pageCount;

            var rez1 = _productRpozitory.FindDeleted().Count;
            ViewBag.deltecoure = rez1;
            var rez2 = _productRpozitory.FindActie().Count;
            ViewBag.activecource = rez2;
			return View(res);
        }


        public IActionResult AdminCourceCategory(int pageid = 1)
        {
            int take = 5;
            int skip = (pageid - 1) * take;

            var res = categoryRepozitory.Oreder().Skip(skip).Take(take).ToList();
            int CategoryCount = categoryRepozitory.GetAll().Count();
            ViewBag.CategoryCount = CategoryCount;
            ViewBag.PageId = pageid;
            ViewBag.Take = take;
            var pageCount = (CategoryCount / take);
            ViewBag.pageCount = pageCount;



			
            return View(res);


			

           
        }

       

        public IActionResult AdminAddCource()
        {
            var rez = categoryRepozitory.GetAll().ToList();
			ViewBag.Categories = rez;
            var rez1 = levelRepozitory.GetAll(). ToList();
            ViewBag.levels = rez1;
          
			return View();  
        }

        [HttpPost]
        public IActionResult AdminAddCource(MyProduct product ,IFormFile img)
        {


			var rez =categoryRepozitory.GetAll().ToList();
			ViewBag.Categories = rez;
			var rez1 = levelRepozitory.GetAll().ToList();
			ViewBag.levels = rez1;

			if (!ModelState.IsValid) { 
             return View(product);
            }
		

			if (img != null)
			{
				bool isConversionSuccessful = false;
				using (var stream = new MemoryStream() ) { 
				
					img.CopyTo(stream);
					
					try
					{
						var bitmap = new Bitmap(Image.FromStream(stream));
						var convert = new Bitmap(bitmap);
						convert.Save(stream, ImageFormat.Png);
						 isConversionSuccessful = true;
					}
					catch
					{
						 isConversionSuccessful = false;
					}

					
				    if(isConversionSuccessful)
					{

						string avatarpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars/");

						string Avatarname = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
						string SavePath = Path.Combine(avatarpath, Avatarname);
						using (var streem = System.IO.File.Create(SavePath))
						{
							img.CopyTo(streem);
						}
						var p = new MyProduct()
						{
							ProductName = product.ProductName,
							Price = product.Price,
							DurationData = product.DurationData,
							CountVideo = product.CountVideo,
							Description = product.Description,
							Createdate = DateTime.Now,
							ModifiedDate = DateTime.Now,
							Avatar = Avatarname,
							MYlevelID = product.MYlevelID,
							MyCategoryID = product.MyCategoryID

						};
						_productRpozitory.Add(p);
						_productRpozitory.Save();

						return RedirectToAction("AdminCourceList");

					}
					else
					{
						ModelState.AddModelError("Avatar", "لطفا فایل مورد نظر را به درستی انتخاب نمایید");
						return View();
					}
				
				
				
				
				
				}

            }
            else
            {
                ModelState.AddModelError("Avatar" ,"لطفا عکس دوره را انتخاب نمایید");
				return View();
			}
		            
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = categoryRepozitory.FindAllById(id);

            if (category != null)
            {
				category.Isdelete = true;
            }
            categoryRepozitory.Save();
            return RedirectToAction("AdminCourceCategory");
        }

        public IActionResult RecoverCategory(int id)
        {
            var category = categoryRepozitory.FindAllById(id);

			if (category != null)
            {
                category.Isdelete = false;
            }
			categoryRepozitory.Save();
			return RedirectToAction("AdminCourceCategory");
        }



		public IActionResult DeleteProduct(int id)
		{
			var product = _productRpozitory.ProductFindById(id);

			if (product != null)
			{
				product.Isdelete = true;
			}
            _productRpozitory.Save();
			return RedirectToAction("AdminCourceList");
		}

		public IActionResult RecoverProduct(int id)
		{
            var category = _productRpozitory.ProductFindById(id);

			if (category != null)
			{
				category.Isdelete = false;
			}
            _productRpozitory.Save();
			return RedirectToAction("AdminCourceList");
		}


		public IActionResult ShowVisit(int pageid = 1)
		{
			int take = 10;
			int skip = (pageid - 1) * take;
			var list = visitRepozitory.GetAll().Skip(skip).Take(take).ToList();
			
			
			int visitcount = visitRepozitory.counAll();
			ViewBag.visitcount = visitcount;
			ViewBag.pageid = pageid;
			ViewBag.take = take;
			var pageCount = (visitcount / take);
			ViewBag.pageCount = pageCount;
			
			return View(list);
		}
	}

   
}
