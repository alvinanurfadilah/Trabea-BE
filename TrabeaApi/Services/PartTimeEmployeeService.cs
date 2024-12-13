using Microsoft.AspNetCore.Mvc.Rendering;
using TrabeaApi.DTOs.PartTimeEmployees;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;
using static TrabeaApi.DTOs.Constant;

namespace TrabeaApi.Services;

public class PartTimeEmployeeService
{
    private readonly IPartTimeEmployeeRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public PartTimeEmployeeService(IPartTimeEmployeeRepository repository, IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public List<SelectListItem> EducationDropdown()
    {
        List<SelectListItem> education = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = "Elementary",
                Text = "Elementary School"
            },
            new SelectListItem()
            {
                Value = "Junior",
                Text = "Junior High School"
            },
            new SelectListItem()
            {
                Value = "HighSchool",
                Text = "High School"
            },
            new SelectListItem()
            {
                Value = "Bachelor",
                Text = "University - Bachelor"
            },
            new SelectListItem()
            {
                Value = "Master",
                Text = "University - Master"
            },
            new SelectListItem()
            {
                Value = "Doctorate",
                Text = "University - Doctorate"
            }
        };

        return education;
    }

    public PartTimeEmployeeIndexDTO Get(int pageNumber, string fullName)
    {
        var model = _repository.Get(pageNumber, PageSize, fullName).Select(ptEmp => new PartTimeEmployeeDTO()
        {
            Id = ptEmp.Id,
            FullName = ptEmp.FirstName + " " + ptEmp.LastName,
            PersonalEmail = ptEmp.PersonalEmail,
            WorkEmail = ptEmp.WorkEmailNavigation.Email,
            PersonalPhoneNumber = ptEmp.PersonalPhoneNumber,
            JoinDate = ptEmp.JoinDate,
        }).ToList();

        return new PartTimeEmployeeIndexDTO()
        {
            PartTimeEmployees = model,
            Pagination = new DTOs.PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count(fullName)
            },
            FullName = fullName,
        };
    }

    public PartTimeEmployee Get(int id)
    {
        return _repository.Get(id);
    }

    public void Insert(PartTimeEmployeeInsertDTO dto)
    {
        var getRole = _roleRepository.Get(3);
        List<Role> roles = new List<Role>();
        roles.Add(getRole);
        var modelUser = new User()
        {
            Email = dto.FirstName.ToLower() + "." + dto.LastName.ToLower() + "@trabea.co.id",
            Password = BCrypt.Net.BCrypt.HashPassword("Trabea123"),
            Roles = roles
        };

        _userRepository.Insert(modelUser);

        var model = new PartTimeEmployee()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PersonalEmail = dto.PersonalEmail,
            PersonalPhoneNumber = dto.PersonalPhoneNumber,
            LastEducation = dto.LastEducation,
            Address = dto.Address,
            OnGoingEducation = dto.OnGoingEducation,
            JoinDate = DateTime.Now,
            WorkEmail = modelUser.Email
        };

        _repository.Insert(model);
    }

    public void Update(PartTimeEmployeeUpdateDTO dto)
    {
        var model = _repository.Get(dto.Id);
        model.FirstName = dto.FirstName;
        model.LastName = dto.LastName;
        model.PersonalEmail = dto.PersonalEmail;
        model.PersonalPhoneNumber = dto.PersonalPhoneNumber;
        model.Address = dto.Address;
        model.LastEducation = dto.LastEducation;
        model.OnGoingEducation = dto.OnGoingEducation;

        _repository.Update(model);
    }
}
