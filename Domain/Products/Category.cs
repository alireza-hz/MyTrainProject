using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
	public class Category
	{
		
			[Key]
			public int MyCategoryID { get; set; }
			[MaxLength(50)]

			[Required(ErrorMessage = "لطفا دسته بندی را وارد نمایید")]
			public string description { get; set; }

			public List<MyProduct>? products { get; set; }

			public DateTime Createdate { get; set; }

			public DateTime ModifiedDate { get; set; }

			public bool Isdelete { get; set; }
		
	}
}
