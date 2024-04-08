using Domain.Products;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Contract
{
    public interface IProductRpozitory : IGenericRepozitory<MyProduct>
    {
		MyProduct ProductFindById(int id);

		MyProduct FindProduct(int id);

        List<MyProduct> FindDeleted();

        List<MyProduct> FindActie();

        int CountPruductById(int id);

        

        List<MyProduct> findlevelpruduct(int id);

		List<MyProduct> findlevelpruductNOWeher();

		List<MyProduct> FindActieWithWehere(string name);


        List<MyProduct> Returnlastcource();

        List<MyProduct> ReturnSimilatorCource(int id,int categotyid);

       
        List<MyProduct> ReturnListproduct(List<int> list);

        int FindFinalPrice(List<int> ints);

     
        List<MyProduct> FindActiveCourceWithcaregory(int categoryId);

        MyProduct search(string input);
       
	}
}
