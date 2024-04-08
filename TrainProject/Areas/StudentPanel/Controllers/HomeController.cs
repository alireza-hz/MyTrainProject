using Application.Convert;
using DataLayer.Contract;
using DataLayer.Repozitory;
using Domain.Products;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace TrainProject.Areas.StudentPanel.Controllers
{
	[Area("StudentPanel")]
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IUserRepozirory _userRepozirory;
		private readonly IitemRepozitory itemRepozitory;

		public HomeController(IUserRepozirory userRepozirory,IitemRepozitory itemRepozitory)
		{
			_userRepozirory = userRepozirory;
			this.itemRepozitory = itemRepozitory;
		}
		public IActionResult Index(int pageid = 1)
		{
			int userId = int.Parse(User.FindFirstValue("UserId"));
			int take = 5;
			int skip = (pageid - 1) * take;
			var rez = itemRepozitory.finduserofcoure(userId).Skip(skip).Take(take);
			var rez1 = itemRepozitory.finduserofcoure(userId).Count();
			int productCount = rez1;
			ViewBag.productCount = productCount;
			ViewBag.PageId = pageid;
			ViewBag.Take = take;
			var pageCount = (productCount / take);
			ViewBag.pageCount = pageCount;
			ViewBag.info = rez;
			

			return View("Index");
		}
		



		public IActionResult EditProfile()
		{

			int userId = int.Parse(User.FindFirstValue("UserId"));
			var user = _userRepozirory.finduserbyId(userId);

			return View(user);
		}


		[HttpPost]
		public IActionResult EditProfile(MyUser user, IFormFile imgup)
		{
			int userId = int.Parse(User.FindFirstValue("UserId"));
			var currentuser = _userRepozirory.finduserbyId(userId);

			currentuser.UserName = user.UserName;
			currentuser.Address = user.Address;
			currentuser.ForMe = user.ForMe;
			currentuser.ModifiedDate = DateTime.Now;

			if (imgup != null)
			{
				bool isConversionSuccessful = false;
				using (var stream = new MemoryStream())
				{

					imgup.CopyTo(stream);
					
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



					if (isConversionSuccessful)
					{


						string avatarpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Avatars/");
						if (currentuser.Avatar != null)
						{
							if (System.IO.File.Exists(Path.Combine(avatarpath, currentuser.Avatar)))
							{
								System.IO.File.Delete(Path.Combine(avatarpath, currentuser.Avatar));
							}
						}
						string Avatarname = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);

						string SavePath = Path.Combine(avatarpath, Avatarname);
						using (var streem = System.IO.File.Create(SavePath))
						{
							imgup.CopyTo(streem);
						}

						currentuser.Avatar = Avatarname;

					}
					else
					{
						ModelState.AddModelError("Avatar", "لطفا فایل مورد نظر را به درستی انتخاب نمایید");
						return View();
					}
					_userRepozirory.Save();
					ViewBag.edit = true;
					return View(currentuser);

				}


			}
			else
			{
				ModelState.AddModelError("Avatar", "لطفا عکس خود را انتخاب نمایید");
				return View();
			}
		
		}



		public IActionResult deleteusr() 
		{
			int userId = int.Parse(User.FindFirstValue("UserId"));
			var user = _userRepozirory.finduserbyId(userId);
			if (user != null)
			{
				user.IsDelete = true;
				_userRepozirory.Save();
			}
			HttpContext.SignOutAsync();
			return Redirect("/Home/index");
		
		}

		
	}
}
