using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Abstract;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductService>().As<IProductService>();
        builder.RegisterType<EfProductDal>().As<IProductDal>();

        builder.RegisterType<CategoryService>().As<ICategoryService>();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

        builder.RegisterType<UserService>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();
    }
}