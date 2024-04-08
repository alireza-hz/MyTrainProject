using DataLayer.Context;
using DataLayer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repozitory
{
    public class GenericRepozitory<T> : IGenericRepozitory<T> where T : class
    {
        private readonly TrainDbContext _context;

        public GenericRepozitory(TrainDbContext context)
        {
          _context = context;
        }
        public void Add(T entity)
        {
           _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
