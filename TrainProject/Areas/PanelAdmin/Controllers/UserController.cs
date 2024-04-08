using DataLayer.Contract;
using DataLayer.Repozitory;
using Domain.Shop;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TrainProject.Areas.PanelAdmin.Controllers
{
	[Area("PanelAdmin")]
    [Authorize]
	public class UserController : Controller
	{
		private readonly IUserRepozirory userRepozitory;
        private readonly IitemRepozitory itemRepozitory;
        private readonly IProductRpozitory productRpozitory;

        public UserController(IUserRepozirory userRepozitory , IitemRepozitory itemRepozitory , IProductRpozitory productRpozitory)
        {
			this.userRepozitory = userRepozitory;
            this.itemRepozitory = itemRepozitory;
            this.productRpozitory = productRpozitory;
        }
        public IActionResult Index(int pageid =1)
		{
			int take = 10;
			int skip = (pageid - 1) * take;
			var res = userRepozitory.GetAll().OrderByDescending(u => u.CreateDate).Skip(skip).Take(take).ToList();		
			int productCount =res.Count();
			ViewBag.productCount = productCount;
			ViewBag.PageId = pageid;
			ViewBag.Take = take;
			var pageCount = (productCount / take);
			ViewBag.pageCount = pageCount;
			
			return View(res);
			
		}

		public IActionResult DeleteUser(int id) 
		{
            var user = userRepozitory.finduserbyId(id);

            if (user != null)
            {
                user.IsDelete = true;
                 HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                }).GetAwaiter().GetResult() ;
            }
            userRepozitory.Save();
            
            return RedirectToAction("Index");
            
		}

        public IActionResult RecoverUser(int id)
        {
            var user = userRepozitory.finduserbyId(id);

            if (user != null)
            {
                user.IsDelete = false;
            }
            userRepozitory.Save();
            return RedirectToAction("Index");
        }


		[Authorize]
		public IActionResult ShowCourceofuser(int pageid=1 )
		{
            int userId = int.Parse(User.FindFirstValue("UserId"));
            int take = 5;
            int skip = (pageid - 1) * take;
			var rez = itemRepozitory.findIDProductAdmin(userId);
            var rez1 = itemRepozitory.finditeminfo(userId, rez).Skip(skip).Take(take);
            int productCount =rez1.Count();
            ViewBag.productCount = productCount;
            ViewBag.PageId = pageid;
            ViewBag.Take = take;
            var pageCount = (productCount / take);
            ViewBag.pageCount = pageCount;
            ViewBag.info = rez1;

            return View();
		}


        public IActionResult ConfirmCourc(int Itemid)
        {

            var item = itemRepozitory.find(Itemid);
            if (item != null)
            {
                item.IsConfirm = true;
                itemRepozitory.Update(item);
                itemRepozitory.Save();
            }
           return  RedirectToAction("ShowCourceofuser");
        }


        public IActionResult RejectCource(int Itemid)
        {
            var item = itemRepozitory.find(Itemid);
            if (item != null)
            {
                item.IsConfirm = false;
                itemRepozitory.Update(item);
                itemRepozitory.Save();
            }
            return RedirectToAction("ShowCourceofuser");
        }
    }
}
