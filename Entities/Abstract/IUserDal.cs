using Core.DataAccess;
using Entities.Concrete;

namespace Entities.Abstract;

public interface IUserDal : IEntityRepository<User>
{
    List<OperationClaim> GetClaims(User user);
}