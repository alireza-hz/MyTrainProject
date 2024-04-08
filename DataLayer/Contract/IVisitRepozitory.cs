using Application.ViewModel;
using Domain.Products;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Contract
{
	public interface IVisitRepozitory : IGenericRepozitory<Visit>
	{
		List<int> CountVisit();

		List<VisitVM> listvisitdaily(List<int> ints);

		List<VisitVM> listvisitMonthly(List<int> ints);
		int counAll();

		List<DateTime> dateTimes();

	}
}
