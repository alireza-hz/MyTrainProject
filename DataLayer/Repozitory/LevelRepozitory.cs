using DataLayer.Context;
using DataLayer.Contract;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repozitory
{
	public class LevelRepozitory :GenericRepozitory<Level>,ILevelRepozitory
	{
		private readonly TrainDbContext context;

		public LevelRepozitory(TrainDbContext context):base(context)
        {
			this.context = context;
		}

		public List<string> findlevel()
		{
			return context.Levels.Select(c=>c.Description).ToList();
		}
	}
}
