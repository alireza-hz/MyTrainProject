using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
	public class Visit
	{
        [Key]
        public int visitid { get; set; }
        public string IPAddress { get; set; }
        public DateTime timevisit { get; set; }

    }
}
