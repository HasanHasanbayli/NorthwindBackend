using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
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

    public IDataResult<List<Product>> GetList()
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
    }

    public IDataResult<List<Product>> GetListByCategory(int categoryId)
    {
        return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
    }

    [ValidationAspect(typeof(ProductValidator), Priority = 1)]
    public IResult Add(Product product)
    {
        _productDal.Add(product);
        _productDal.Update(product);

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
}