using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class ProductService : IProductService
{
    private readonly IProductDal _productDal;

    public ProductService(IProductDal productDal)
    {
        _productDal = productDal;
    }

    public IDataResult<Product?> GetById(int productId)
    {
        return new SuccessDataResult<Product?>(_productDal.Get(p => p.ProductId == productId));
    }

    [SecuredOperation("Admin")]
    [PerformanceAspect(6)]
    [LogAspect(typeof(FileLogger))]
    public IDataResult<List<Product>> GetList()
    {
        // Thread.Sleep(6000);
        return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
    }

    [CacheAspect(duration: 60)]
    // [LogAspect(typeof(FileLogger))]
    public IDataResult<List<Product>> GetListByCategory(int categoryId)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
    }

    [ValidationAspect(typeof(ProductValidator), Priority = 1)]
    [CacheRemoveAspect("IProductService.Get")]
    public IResult Add(Product product)
    {
        _productDal.Add(product);

        return new Result(true, Messages.ProductAdded);
    }

    public IResult Update(Product product)
    {
        _productDal.Update(product);

        return new Result(true, Messages.ProductUpdated);
    }

    public IResult Delete(Product product)
    {
        _productDal.Delete(product);

        return new Result(true, Messages.ProductDeleted);
    }

    [TransactionScopeAspect]
    public IResult TransactionalOperation(Product product)
    {
        _productDal.Update(product);
        _productDal.Add(product);
        return new Result(true, Messages.ProductUpdated);
    }
}