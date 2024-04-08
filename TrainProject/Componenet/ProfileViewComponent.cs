using DataLayer.Contract;
using Microsoft.AspNetCore.Mvc;

using Domain.Users;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;
using Application.ViewModel;
using System.Net.WebSockets;

namespace TrainProject.Componenet
{
	public class ProfileViewComponent: ViewComponent
	{
		private readonly IUserRepozirory _userRepozirory;
		private readonly IitemRepozitory itemRepozitory;

		public ProfileViewComponent(IUserRepozirory userRepozirory , IitemRepozitory itemRepozitory)
		{
			_userRepozirory = userRepozirory;
			this.itemRepozitory = itemRepozitory;
		}

		

		public async Task<IViewComponentResult> InvokeAsync()
		{

			int userId = int.Parse(HttpContext.User.FindFirstValue("UserId"));
			var user = _userRepozirory.finduserbyId(userId);
			var countCource = itemRepozitory.CountCourceUser(userId);
		
			var info = new PanelUserUpVm()
			{
				createdate = user.CreateDate,
				countCource = countCource,
			
				username = user.UserName,
				Avater =user.Avatar,
			};

			return View(info);
		}
	}
}
