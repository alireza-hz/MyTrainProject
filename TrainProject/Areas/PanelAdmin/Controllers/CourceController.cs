 using Application.ViewModel;
using DataLayer.Context;
using DataLayer.Contract;
using DataLayer.Repozitory;
using Domain.Products;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TrainProject.Areas.PanelAdmin.Controllers
{
	[Area("PanelAdmin")]
	[Authorize]
	public class CourceController : Controller
	{
		private readonly ICategoryRepozitory categoryRepozitory;
		private readonly IProductRpozitory _productRpozitory;
		private readonly ILevelRepozitory levelRepozitory;

		public CourceController(ICategoryRepozitory categoryRepozitory , IProductRpozitory productRpozitory ,ILevelRepozitory levelRepozitory )
        {
			this.categoryRepozitory = categoryRepozitory;
			_productRpozitory = productRpozitory;
			this.levelRepozitory = levelRepozitory;
		}
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddCategory()
		{
			return View();	
		}

		[HttpPost]
        public IActionResult AddCategory(Category categorie)
        {
		
			if (!ModelState.IsValid)
			{
				return View(categorie);
			}			
			var des = categorie.description.Trim();	
			if (categoryRepozitory.isExist(des))
			{
				ModelState.AddModelError("description", "این دسته بندی قبلا انتخاب شده است");
				return View(categorie);

			}

			var catogory = new Category()
			{
				description = categorie.description,
				Createdate = DateTime.Now,
				ModifiedDate = DateTime.Now,
			};
			categoryRepozitory.Add(catogory);
			categoryRepozitory.Save();

			return RedirectToAction("AdminCourceCategory", "home");
        }
		
		public IActionResult EditCategory(int categoryId)
		{
			
			var category = categoryRepozitory.FindbyId(categoryId);
			return View(category);
		}
		[HttpPost]
		public IActionResult EditCategory(Category categorie)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}


			var category = categoryRepozitory.FindbyId(categorie.MyCategoryID);
			if (category == null)
			{
				return Redirect("/");

			}
			category.ModifiedDate = DateTime.Now;
			category.description = categorie.description;


			categoryRepozitory.Save();
            ViewBag.edit = true;
            return RedirectToAction("AdminCourceCategory", "home"); ;
		}


		public IActionResult ShowCategoryWithFilter(int categoryid , int pageid = 1)
		{
			
			int take = 5;
			int skip = (pageid - 1) * take;
			var category = categoryRepozitory.FindbyId(categoryid);
			var count = _productRpozitory.CountPruductById(categoryid);
			var products = _productRpozitory.findlevelpruduct(categoryid).Skip(skip).Take(take).ToList();					
			int categorycount = count;
			ViewBag.categorycount = categorycount;
			ViewBag.pageid= pageid;
			ViewBag.take = take;
			var pageCount = (categorycount / take);
			ViewBag.pageCount = pageCount;

			var rez = new CategoryFilterVM()
			{
				categoryid = categoryid,
				category = category.description,
				countcategoty = count,
				products = products,
			};

			return View(rez);
		}


		public IActionResult EditCource(int productid)
		{
			var rez = categoryRepozitory.GetAll().ToList();
			ViewBag.Categories = rez;
			var rez1 = levelRepozitory.GetAll().ToList();
			ViewBag.levels = rez1;
			var producr = _productRpozitory.FindProduct(productid);
			return View(producr); 
		}

		[HttpPost]
		public IActionResult EditCource(MyProduct product , IFormFile img)
		{
			var rez = categoryRepozitory.GetAll().ToList();
			ViewBag.Categories = rez;
			var rez1 =levelRepozitory.GetAll().ToList();
			ViewBag.levels = rez1;
			if (!ModelState.IsValid)
			{
				return View(product);
			}


			var currentproduct = _productRpozitory.FindProduct(product.MyProductId);
			currentproduct.ModifiedDate = DateTime.Now;
			currentproduct.ProductName = product.ProductName;
			currentproduct.DurationData = product.DurationData;
			currentproduct.CountVideo = product.CountVideo;
			currentproduct.MyCategoryID = product.MyCategoryID;
			currentproduct.MYlevelID = product.MYlevelID;
			currentproduct.Price = product.Price;
			currentproduct.Description = product.Description;
			currentproduct.ModifiedDate = DateTime.Now;

			if (img != null)
			{
				string avatarpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars/");
				if (currentproduct.Avatar != null)
				{
					if (System.IO.File.Exists(Path.Combine(avatarpath, currentproduct.Avatar)))
					{
						System.IO.File.Delete(Path.Combine(avatarpath, currentproduct.Avatar));
					}
				}
				string Avatarname = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);

				string SavePath = Path.Combine(avatarpath, Avatarname);
				using (var streem = System.IO.File.Create(SavePath))
				{
					img.CopyTo(streem);
				}

				currentproduct.Avatar = Avatarname;

			}
	
			_productRpozitory.Save();

			return RedirectToAction("AdminCourceList" , "Home");
		}




		public IActionResult ShowActiveCource(int pageid = 1) 
		{
			int take = 5;
			int skip = (pageid - 1) * take;
			var res = _productRpozitory.FindActie().OrderByDescending(u => u.Createdate).Skip(skip).Take(take).ToList();
			ViewBag.productCount1 = _productRpozitory.GetAll().Count();
			int productCount = _productRpozitory.FindActie().Count();
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

		public IActionResult ShowDeleteCource(int pageid = 1)
		{
			int take = 5;
			int skip = (pageid - 1) * take;
			var res = _productRpozitory.FindDeleted().OrderByDescending(u => u.Createdate).Skip(skip).Take(take).ToList();
			ViewBag.productCount1 = _productRpozitory.GetAll().Count();
			int productCount = _productRpozitory.FindDeleted().Count();
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

	}
}
