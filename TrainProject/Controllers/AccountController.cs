using Application;
using Application.ViewModel;
using DataLayer.Contract;
using DataLayer.Repozitory;
using Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using RestSharp;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;


namespace TrainProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepozirory _userRepozirory;
		

		public AccountController(IUserRepozirory userRepozirory)
        {
            _userRepozirory = userRepozirory;
			
		}



        public IActionResult Index()
        {
            return View();
        }



        [Route("/Register")]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [Route("/Register")]
        public IActionResult Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userRepozirory.isexistuserByMoblieNumber(register.Moblie))
            {
                ModelState.AddModelError("Moblie", "شماره موبایل وارد شده قبلا ثبت شده است");
                return View();
            }

            MyUser user = new MyUser()
            {
             
                UserName = register.name,
                Mobile = register.Moblie,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
				MyRoleID = 1,
                Avatar = "download.png"

			};
            TempData["user"] = JsonConvert.SerializeObject(user);
            Random rnd = new Random();
            string mycode = rnd.Next(10000, 100000).ToString();
            TempData["confirmcode"] = mycode;

            SendCode sendCode = new SendCode(mycode, register.Moblie);
            sendCode.Send();
            DateTime expirationTime = DateTime.Now.AddMinutes(1);
            TempData["ExpirationTime"] = expirationTime;
            TempData["state"] = "register";
            return RedirectToAction("Confirm");
        }







        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("/login")]
        public IActionResult Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            { return View(login); }

            var user = _userRepozirory.isexistuserByMoblieNumber(login.Moblie);
            if (user == false)
            {
                ModelState.AddModelError("Moblie", "شماره موبایل اشتباه است");
                return View();
            }
            var user1 = _userRepozirory.Isdelete(login.Moblie);
			if (user1 == true)
			{
				ModelState.AddModelError("Moblie", "دسترسی شما به پنل تان مسدود شده است");
				return View();
			}

			var userinfo= _userRepozirory.finduserinfo(login.Moblie);

			Random rnd = new Random();
            string mycode = rnd.Next(10000, 100000).ToString();
            TempData["confirmcode"] = mycode;
            SendCode sendCode = new SendCode(mycode, login.Moblie);
            sendCode.Send();
            DateTime expirationTime = DateTime.Now.AddSeconds(60);
            TempData["ExpirationTime"] = expirationTime;
            TempData["state"] = null;
            TempData["userlogin"] = JsonConvert.SerializeObject(userinfo);
            
            return RedirectToAction("Confirm" , login);

        }



		[HttpGet]
		[Route("confirm")]
		public IActionResult Confirm()
		{

			return View();
		}


		[HttpPost]
		[Route("confirm")]
		public IActionResult Confirm(ConfirmCode confirm)
		{

			if (!ModelState.IsValid)
			{
				return View();
			}
			else
			{
				string mycode = TempData["Confirmcode"].ToString();
				DateTime expirationTime = (DateTime)TempData["ExpirationTime"];
				TempData.Keep();
				if (DateTime.Now <= expirationTime && mycode == confirm.Code)
				{

					if (TempData["state"] != null)
					{
						string userJson = TempData["user"] as string;
						MyUser user1 = JsonConvert.DeserializeObject<MyUser>(userJson);
						_userRepozirory.Add(user1);
						_userRepozirory.Save();
						

                        return RedirectToAction("Index", "Home", new { area = "StudentPanel" });

                    }
					else
					{
						string userJsonLogin = TempData["userlogin"] as string;
						MyUser userLogin = JsonConvert.DeserializeObject<MyUser>(userJsonLogin);
						var claims = new List<Claim>() {

						new Claim(ClaimTypes.Name ,userLogin.UserName ),
						new Claim(ClaimTypes.MobilePhone ,userLogin.Mobile ),
						new Claim ("UserId",userLogin.MYUserID.ToString()),
                        new Claim("Avatar",userLogin.Avatar),
                        new Claim("Role",userLogin.MyRoleID.ToString()),
                        
                        

						};
						var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
						var principal = new ClaimsPrincipal(identity);
						var propertize = new AuthenticationProperties()
						{
							IsPersistent = true,
						};
						HttpContext.SignInAsync(principal, propertize);
                        
                       
                        return RedirectToAction("Index", "Home", new { area = "StudentPanel" });				}


				}
				else if (DateTime.Now > expirationTime)
				{
					ModelState.AddModelError("code", "کد منقضی شده است");
					return View("login");
				}
				else
				{
					ModelState.AddModelError("code", "کد وارد شده اشتباه می باشد");
					return View();
				}

			}



		}


		[Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }
    }
}
