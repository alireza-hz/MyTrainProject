using Domain.Products;
using Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop
{
	public class Item
	{
		[Key]
		
		public int ItemId { get; set; }

        public int MYUserID { get; set; }
        public int MyProductId { get; set; }

        [ForeignKey("MYUserID")]  
        public MyUser? MyUser { get; set; }

        [ForeignKey("MyProductId")]
        public MyProduct? MyProduct { get; set; }


		public bool IsConfirm { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
