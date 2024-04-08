using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    public class Level
    {
        [Key]
        public int MYlevelID { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }

        public List<MyProduct>? Products { get; set;}

    }
}
