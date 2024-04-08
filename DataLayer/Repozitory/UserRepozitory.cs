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
    public class UserRepozitory : GenericRepozitory<MyUser>, IUserRepozirory
    {
        private readonly TrainDbContext _context;

        public UserRepozitory(TrainDbContext context) : base(context)
        {
           _context = context;
        }

		public int CountuserActive()
		{
			return _context.MyUsers.Where(c=>c.IsDelete==false).Count();
		}

		public MyUser finduserbyId(int id)
		{
            return _context.MyUsers.Find(id);
		}

		public MyUser finduserinfo(string moblieNumber)
		{
			return _context.MyUsers.First(u => u.Mobile == moblieNumber);
		}

		public bool Isdelete(string moblieNumber)
		{
			return _context.MyUsers.Any(u => u.Mobile == moblieNumber & u.IsDelete ==true);
		}

		public bool isexistuserByMoblieNumber(string moblieNumber)
        {
            return _context.MyUsers.Any( u => u.Mobile ==  moblieNumber);
        }
    }
}
