using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaApi.Services;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User Get(string email)
    {
        return _repository.Get(email);
    }
}
