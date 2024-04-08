using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
	public class ProductVm
	{
        public int categoryid { get; set; }

        public List<MyProduct>? myProducts { get; set; }

        public List<InfoCategory> InfoCategory { get; set; }
    }
}
