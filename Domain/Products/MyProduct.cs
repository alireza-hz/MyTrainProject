using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Shop;

namespace Domain.Products
{
	public class MyProduct
	{

		[Key]
		public int MyProductId { get; set; }

		public int MyCategoryID { get; set; }
		public int MYlevelID { get; set; }
		
		[MaxLength(100)]

		[Required(ErrorMessage = "لطفا نام دوره را وارد نمایید")]
		public string ProductName { get; set; }


		[Required(ErrorMessage = "لطفا قیمت را وارد نمایید")]
		public int Price { get; set; }

		[Required(ErrorMessage = "لطفا مدت زمان را وارد نمایید")]
		public string DurationData { get; set; }


		[Required(ErrorMessage = "لطفا تعداد ویدئو را وارد نمایید")]
		public string? CountVideo { get; set; }

		[Required(ErrorMessage = "توضیحات را وارد نمایید")]
		public string Description { get; set; }

		public DateTime Createdate { get; set; }

		public DateTime ModifiedDate { get; set; }

		public bool Isdelete { get; set; }


		public string? Avatar { get; set; }


		[ForeignKey("MYlevelID")]
		public Level? Level { get; set; }

		[ForeignKey("MyCategoryID")]
		public Category? Category { get; set; }




		[InverseProperty("MyProduct")]
		public List<Item>? Item { get; set; }
    }
}
