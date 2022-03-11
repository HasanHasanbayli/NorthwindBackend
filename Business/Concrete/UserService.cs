using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Abstract;

namespace Business.Concrete;

public class UserService : IUserService
{
    private readonly IUserDal _userDal;

    public UserService(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public List<OperationClaim> GetClaims(User user)
    {
        return _userDal.GetClaims(user);
    }

    public void AddUser(User user)
    {
        _userDal.Add(user);
    }

    public User? GetByMail(string email)
    {
        return _userDal.Get(u => u.Email == email);
    }
}