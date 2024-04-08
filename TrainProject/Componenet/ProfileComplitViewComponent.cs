using DataLayer.Contract;
using Microsoft.AspNetCore.Mvc;

using Domain.Users;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TrainProject.Componenet
{
	public class ProfileComplitViewComponent : ViewComponent
	{
		private readonly IUserRepozirory _userRepozirory;

		public ProfileComplitViewComponent(IUserRepozirory userRepozirory)
		{
			_userRepozirory = userRepozirory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			
			int persent = 0;
			int userId = int.Parse(HttpContext.User.FindFirstValue("UserId"));
			var user = _userRepozirory.finduserbyId(userId);
			


			if (string.IsNullOrEmpty(user.UserName))
			{
				ViewData["UserName"] = null;

			}
			else
			{
				persent += 25;
				ViewData["UserName"] = user.UserName;
			}
			if (string.IsNullOrEmpty(user.Address))
			{
				ViewData["Address"] = null;

			}
			else
			{
				ViewData["Address"] = user.Address;
				persent += 25;
			}
			if (string.IsNullOrEmpty(user.ForMe))
			{
				ViewData["ForMe"] = null;

			}
			else
			{
				ViewData["ForMe"]= user.ForMe;
				persent += 25;
			}

			if (string.IsNullOrEmpty(user.Avatar))
			{
				ViewData["Avatar"] = null;

			}
			else
			{
				ViewData["Avatar"] = user.Avatar;
				persent += 25;
			}


				return View(persent);
		}
	}
}
