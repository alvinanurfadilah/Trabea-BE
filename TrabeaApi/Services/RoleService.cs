using Microsoft.AspNetCore.Mvc.Rendering;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaApi.Services;

public class RoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public List<SelectListItem> RoleDropdown()
    {
        var model = _repository.Get();
        return model.Select(role => new SelectListItem()
        {
            Text = role.Name,
            Value = role.Id.ToString()
        }).ToList();
    }
}
