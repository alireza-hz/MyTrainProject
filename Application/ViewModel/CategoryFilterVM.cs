using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Products;
namespace Application.ViewModel
{
    public class CategoryFilterVM
    {
        public int categoryid { get; set; }
        public string category { get; set; }

        public int countcategoty { get; set; }

        public List<MyProduct>?  products { get; set; }
    }
}
