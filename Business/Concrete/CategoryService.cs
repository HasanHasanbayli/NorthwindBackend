using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CategoryService : ICategoryService
{
    private readonly ICategoryDal _categoryDal;

    public CategoryService(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }

    public IDataResult<List<Category>> GetList()
    {
        return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList());
    }
}