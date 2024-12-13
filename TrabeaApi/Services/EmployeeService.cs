using TrabeaApi.DTOs.Employees;
using TrabeaBusiness.Interfaces;
using TrabeaBusiness.Repositories;
using TrabeaDataAccess.Models;
using static TrabeaApi.DTOs.Constant;

namespace TrabeaApi.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public EmployeeService(IEmployeeRepository repository, IRoleRepository roleRepository, IUserRepository userRepository)
    {
        _repository = repository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public EmployeeIndexDTO Get(int pageNumber, string fullName)
    {
        var model = _repository.Get(pageNumber, PageSize, fullName).Select(emp => new EmployeeDTO()
        {
            FullName = emp.FirstName + " " + emp.LastName,
            WorkEmail = emp.WorkEmail,
        });

        return new EmployeeIndexDTO()
        {
            Employees = model.ToList(),
            Pagination = new DTOs.PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count(fullName)
            },
            FullName = fullName
        };
    }

    public Employee Get(int id)
    {
        return _repository.Get(id);
    }

    public void Insert(EmployeeInsertDTO dto)
    {
        var getRole = _roleRepository.Get(dto.RoleId);
        List<Role> roles = new List<Role>();
        roles.Add(getRole);
        var modelUser = new User()
        {
            Email = dto.FirstName.ToLower() + "." + dto.LastName.ToLower() + "@trabea.co.id",
            Password = BCrypt.Net.BCrypt.HashPassword("Trabea123"),
            Roles = roles
        };

        _userRepository.Insert(modelUser);

        var model = new Employee()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            WorkEmail = modelUser.Email,
        };

        _repository.Insert(model);
    }

    public void Update(EmployeeUpdateDTO dto)
    {
        var model = _repository.Get(dto.Id);
        model.FirstName = dto.FirtName;
        model.LastName = dto.LastName;

        var getRole = _roleRepository.Get(dto.RoleId);
        List<Role> roles = new List<Role>();
        roles.Add(getRole);
        var getUser = _userRepository.Get(model.WorkEmail);
        getUser.Roles = roles;

        _repository.Update(model);
        _userRepository.Update(getUser);
    }
}
