
using DataLayer.Context;
using DataLayer.Contract;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repozitory
{
    public class RoleRepozitory : GenericRepozitory<MyRole>, IRoleRepozirory
    {
        public RoleRepozitory(TrainDbContext context) : base(context)
        {
        }
    }
}
