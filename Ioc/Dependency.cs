using DataLayer.Contract;
using DataLayer.Repozitory;
using Microsoft.Extensions.DependencyInjection;

namespace Ioc
{
    public static class Dependency
	{
        public static void RegisterService(this IServiceCollection  services)
        {
			services.AddScoped<IUserRepozirory, UserRepozitory>();
			services.AddScoped<IRoleRepozirory, RoleRepozitory>();
			services.AddScoped<IProductRpozitory, ProductRepozitory>();
			services.AddScoped<ICategoryRepozitory, CategoryRepozitory>();
			services.AddScoped<ILevelRepozitory, LevelRepozitory>();
			services.AddScoped<IitemRepozitory, ItemRepozitory>();
			services.AddScoped<IVisitRepozitory, VisitRepozitory>();
		}

    }
}
