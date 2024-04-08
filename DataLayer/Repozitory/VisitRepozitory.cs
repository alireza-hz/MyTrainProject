using Application.Convert;
using Application.ViewModel;
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
	public class VisitRepozitory : GenericRepozitory<Visit> , IVisitRepozitory
	{
		private readonly TrainDbContext context;

		public VisitRepozitory(TrainDbContext context):base(context) 
		{
			this.context = context;
		}

		public int counAll()
		{
			return context.visits.Count();
		}

		public List<int> CountVisit()
		{
			return context.visits.Select(c=>c.visitid).ToList();
		}

		public List<DateTime> dateTimes()
		{
			return context.visits.Select(c=>c.timevisit).ToList();
		}

		public List<VisitVM> listvisitdaily(List<int> ints)
		{
			return context.visits.Select(c => new VisitVM
			{
				IPAddress = c.IPAddress,
				timevisit = c.timevisit.Toshamsi()
			}).ToList();
			
		}

		public List<VisitVM> listvisitMonthly(List<int> ints)
		{
			return context.visits.Select(c => new VisitVM
			{
				IPAddress = c.IPAddress,
				timevisit = c.timevisit.ToshamsiGetMounth()
			}).ToList();

		}
	}
}
