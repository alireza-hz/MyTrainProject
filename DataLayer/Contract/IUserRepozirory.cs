using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Contract
{
    public interface IUserRepozirory : IGenericRepozitory<MyUser>
    {
        bool isexistuserByMoblieNumber(string moblieNumber);

        MyUser finduserinfo(string moblieNumber);

        MyUser finduserbyId(int id);

        bool Isdelete(string moblieNumber);

         int CountuserActive();
    }
}
