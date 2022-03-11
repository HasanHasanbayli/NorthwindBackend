using Core.Entities.Concrete;

namespace Business.Abstract;

public interface IUserService
{
    List<OperationClaim> GetClaims(User user);

    void AddUser(User user);

    User? GetByMail(string email);
}