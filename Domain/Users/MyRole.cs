using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
	public class MyRole
	{
		
			[Key]
			public int MyRoleID { get; set; }

			[Required]
			[MaxLength(300)]
			public string RoleName { get; set; }
			[Required]
			[MaxLength(300)]
			public string RoleTitle { get; set; }

			public List<MyUser>? Users { get; set; }

			public bool IsDelete { get; set; }
			public DateTime? CreateDate { get; set; }
			public DateTime? ModifiedDate { get; set; }
		

	}
}
