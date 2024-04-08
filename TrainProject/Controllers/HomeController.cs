using Application.Convert;
using Application.ViewModel;
using DataLayer.Context;
using DataLayer.Contract;
using DataLayer.Repozitory;
using Domain.Products;
using Domain.Shop;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using TrainProject.Models;

namespace TrainProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepozitory categoryRepozitory;
        private readonly IProductRpozitory productRpozitory;
        private readonly IUserRepozirory userRepozirory;
        private readonly IitemRepozitory itemRepozitory;
        private readonly ILevelRepozitory levelRepozitory;
        private readonly IVisitRepozitory visitRepozitory;

        public HomeController(ILogger<HomeController> logger, ICategoryRepozitory categoryRepozitory, IProductRpozitory productRpozitory, IUserRepozirory userRepozirory, IitemRepozitory itemRepozitory, ILevelRepozitory levelRepozitory, IVisitRepozitory visitRepozitory)
        {
            _logger = logger;
            this.categoryRepozitory = categoryRepozitory;
            this.productRpozitory = productRpozitory;
            this.userRepozirory = userRepozirory;
            this.itemRepozitory = itemRepozitory;
            this.levelRepozitory = levelRepozitory;
            this.visitRepozitory = visitRepozitory;
        }

        public IActionResult Index()
        {
            //پر کردن جدول visit و امار بازدید 
            var Ipaddress = HttpContext.Connection.RemoteIpAddress.ToString();
            //تعریف session برای اینکه کاربر با رفرش به بازدید اضافه نکند
            if (HttpContext.Session.GetString("Visited") != Ipaddress)
            {
                var visit = new Visit()
                {
                    IPAddress = Ipaddress,
                    timevisit = DateTime.Now,
                };
                HttpContext.Session.SetString("Visited", Ipaddress);
                visitRepozitory.Add(visit);
                visitRepozitory.Save();
            }
            //پر کردن همه دسته بندی  ها و همچنین دسته بندی های محبوب در صفحه اول 
            var rez = categoryRepozitory.FindActive();
            var rez1 = categoryRepozitory.FindActive().Select(c => c.description).Take(4).ToList();
            for (int i = 0; i < rez1.Count; i++)
            {
                string name = "ViewData" + i;
                ViewData[name] = rez1[i];
            }
            ViewBag.Allcategory = rez;
            //پر کردن دسته بندی های محبوب
            if (rez1.Count != 0)
            {
                ViewBag.product0 = productRpozitory.FindActieWithWehere(rez1[0]); ;
                ViewBag.product1 = productRpozitory.FindActieWithWehere(rez1[1]); ;
                ViewBag.product2 = productRpozitory.FindActieWithWehere(rez1[2]); ;
                ViewBag.product3 = productRpozitory.FindActieWithWehere(rez1[3]);
            }
            var rez2 = productRpozitory.FindActie().Count();
            ViewBag.count = rez2;

            //دریافت مقدار سرچ کاربر از IActionResult Search 
            string notsearch = TempData["notsearch"] as string;
            string rezsearch = TempData["search"] as string;
            
            if (notsearch != null)
            {
                var not = JsonConvert.DeserializeObject<string>(notsearch);
                ViewBag.not = not;
            }

            if (rezsearch != null)
            {
                MyProduct myProduct = JsonConvert.DeserializeObject<MyProduct>(rezsearch);
                ViewBag.search = myProduct;
            }

            return View();
        }


        public IActionResult Search(Search search)
        {
            //دریافت مقدار سرچ کاربر از صفحه اصلی
            if (search.input == null)
            {

                return RedirectToAction("Index");
            }
            else
            {
                //پیدا کردن مقادیر مرتبط با سرچ کاربر

               string input = search.input.Trim();
                var rez = productRpozitory.search(input);
                if (rez != null)
                {
                    TempData["search"] = JsonConvert.SerializeObject(rez);
                }
                else
                {
                    var rez1 = "چیزی یافت نشده";
                    TempData["notsearch"] = JsonConvert.SerializeObject(rez1);


                }
                
            }
          
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult ShowCource(int id, int categoryid)
        {
            //پیدا کردن مقادیر مرتبط به هر دوره و نمایش ان
            var rez = productRpozitory.FindProduct(id);
            var rez2 = productRpozitory.Returnlastcource();
            ViewBag.lastcource = rez2;
            var rez3 = productRpozitory.ReturnSimilatorCource(id, categoryid);
            if (rez3.Count == 0)
            {
                ViewBag.similator = null;
            }
            else
            {
                ViewBag.similator = rez3;
            }
            return View(rez);
        }



        [Authorize]
        public IActionResult AddtoItem(int productid = 0)
        {
            int userId = int.Parse(User.FindFirstValue("UserId"));

            
            if (productid == 0)
            {
                //اضافه شدن به سبد خرید 
                var rez = itemRepozitory.findIDProduct(userId);
                var rez1 = productRpozitory.ReturnListproduct(rez);
                ViewBag.item = rez1;
                //پیدا کردن جمع کل
                var rez4 = productRpozitory.FindFinalPrice(rez);
                ViewBag.sum = rez4;
                return View();
            }
            else
            {
                //اضافه شدن به سبد خرید
                var chechitem = itemRepozitory.IsExsistCourse(userId, productid);
                if (!chechitem)
                {
                    var item = new Item()
                    {
                        MyProductId = productid,
                        MYUserID = userId,
                        IsConfirm = false,
                        CreateDate = DateTime.Now,
                    };
                    itemRepozitory.Add(item);
                    itemRepozitory.Save();
                }
                else
                {
                    ViewBag.checkitem = "true";
                }
                //پیدا کردن جمع کل
                var rez2 = itemRepozitory.findIDProduct(userId);
                var rez3 = productRpozitory.ReturnListproduct(rez2);
                var rez4 = productRpozitory.FindFinalPrice(rez2);
                ViewBag.sum = rez4;
                ViewBag.item = rez3;

            }



            return View();

        }
        [Authorize]
        public void deleteItem(int productid)
        {
            int userId = int.Parse(User.FindFirstValue("UserId"));
            var item = itemRepozitory.findItemForDelete(userId, productid);
            if (item != null)
            {
                itemRepozitory.Delete(item);
                itemRepozitory.Save();
            }
            Response.Redirect("/Home/AddtoItem?productid=0");

        }

        [Authorize]
        public IActionResult AddToTransaction()
        {
            return View();
        }


        public IActionResult ShowGridCource(int categoryid, int pageid = 1)
        {
            //نشان دادن صفحه محصول ها 
            ViewBag.categorynae = categoryRepozitory.FindName(categoryid);
            int take = 9;
            int skip = (pageid - 1) * take;
            var rez = productRpozitory.FindActiveCourceWithcaregory(categoryid).Skip(skip).Take(take);
            var rez1 = productRpozitory.CountPruductById(categoryid);
            int productCount = rez1;
            ViewBag.productCount = productCount;
            ViewBag.PageId = pageid;
            ViewBag.Take = take;
            var pageCount = (productCount / take);
            ViewBag.pageCount = pageCount;
            ViewBag.info = rez;
            ViewBag.categoryid = categoryid;

            var rez2 = categoryRepozitory.FindActive();
            
            var idcategory = new List<int>();
            foreach (var item in rez2)
            {
                idcategory.Add(item.MyCategoryID);
            }
            var info = new List<InfoCategory>();

            foreach (var item in idcategory)
            {
                var category = categoryRepozitory.FindName(item);
                var count = productRpozitory.CountPruductById(item);
                var infoCategory = new InfoCategory()
                {
                    namecategory = category,
                    countcategory = count,
                    categoryid = item,
                };
                info.Add(infoCategory);
            }
            ViewBag.infocategory = info;
            var product = new ProductVm()
            {
                categoryid = categoryid,
                InfoCategory = info,
            };

            return View(product);

        }






        public IActionResult filter(int categoryid, int pageid = 1)
        {

            ViewBag.categorynae = categoryRepozitory.FindName(categoryid);
            int take = 9;
            int skip = (pageid - 1) * take;
            var rez = productRpozitory.FindActiveCourceWithcaregory(categoryid).Skip(skip).Take(take);
            var rez1 = productRpozitory.CountPruductById(categoryid);
            int productCount = rez1;
            ViewBag.productCount = productCount;
            ViewBag.PageId = pageid;
            ViewBag.Take = take;
            var pageCount = (productCount / take);
            ViewBag.pageCount = pageCount;
            ViewBag.info = rez;
            ViewBag.categoryid = categoryid;
            var product = new ProductVm()
            {
                categoryid = categoryid,
            };

            var rez2 = categoryRepozitory.FindActive();
            var idcategory = new List<int>();
            foreach (var item in rez2)
            {
                idcategory.Add(item.MyCategoryID);
            }
            var info = new List<InfoCategory>();

            foreach (var item in idcategory)
            {
                var category = categoryRepozitory.FindName(item);
                var count = productRpozitory.CountPruductById(item);
                var infoCategory = new InfoCategory()
                {
                    namecategory = category,
                    countcategory = count,
                    categoryid = item,
                };
                info.Add(infoCategory);
            }
            ViewBag.infocategory = info;
            return View(product);
        }



    }
}
