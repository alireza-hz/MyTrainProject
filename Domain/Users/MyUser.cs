using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shop;

namespace Domain.Users
{
	public class MyUser
	{

		[Key]
		public int MYUserID { get; set; }
		
		

		[MaxLength(300)]
		public string? UserName { get; set; }

		[MaxLength(100)]
		public string? Mobile { get; set; }

		[MaxLength(50)]
		public string Avatar { get; set; }

		public string? Address { get; set; }

		public string? ForMe { get; set; }


		public bool IsDelete { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime ModifiedDate { get; set; }




		public int MyRoleID { get; set; }

		[ForeignKey("MyRoleID")]
		public MyRole? Role { get; set; }



		[InverseProperty("MyUser")]
		public List<Item>? Item { get; set; }


	}
}
