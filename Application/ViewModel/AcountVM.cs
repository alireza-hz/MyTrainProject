using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
	public class RegisterVM
	{
		[Required(ErrorMessage = "لطفا نام کاربری را وارد نمایید")]
		[Display(Name = "نام کاربری")]
		public string? name { get; set; }
		[Required(ErrorMessage = "لطفا شماره موبایل خود را وارد نمایید")]
		[Display(Name = "شماره موبایل")]
		[MaxLength(100)]
		public string Moblie { get; set; }
   
    }
	public class LoginVM
	{

	
		[Required(ErrorMessage ="لطفا شماره موبایل خود را وارد نمایید")]
		[Display(Name = "شماره موبایل")]
		[MaxLength(100)]
		public string Moblie { get; set; }

        public bool Remmeber { get; set; }
    }


	public class ConfirmCode
	{
		
		[Required(ErrorMessage = "رمز پیامک شده را وارد نمایید")]
		[Display(Name = "رمز یکبار مصرف")]
		[MaxLength(5)]
		public string Code { get; set; }
	}
}


