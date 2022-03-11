using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace Entities.Abstract;

public interface IUserDal : IEntityRepository<User>
{
    List<OperationClaim> GetClaims(User user);
}