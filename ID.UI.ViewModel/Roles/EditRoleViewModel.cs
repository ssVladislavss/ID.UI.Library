﻿using ID.UI.Core.Roles;
using ID.UI.ViewModel.Claims;
using ID.UI.ViewModel.Roles.Validators;

namespace ID.UI.ViewModel.Roles
{
    public class EditRoleViewModel
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        public List<ClaimViewModel> Claims { get; set; } = new List<ClaimViewModel>();

        public EditRoleViewModelValidator Validator { get; set; } = new EditRoleViewModelValidator();

        public EditRoleViewModel() { }
        public EditRoleViewModel(RoleModel role)
        {
            if(role != null)
            {
                RoleId = role.RoleId;
                RoleName = role.RoleName;
                Claims = role.Claims.Select(x => new ClaimViewModel { Type = x.Type, Value = x.Value }).ToList();
            }
        }
    }
}
