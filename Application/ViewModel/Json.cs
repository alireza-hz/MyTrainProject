using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class Variable
    {
        public string name { get; set; }
    }

    public class Example
    {
        public string code { get; set; }
        public string sender { get; set; }
        public string recipient { get; set; }
        public Variable variable { get; set; }
    }


}


