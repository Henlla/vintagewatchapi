using System.Data.Entity;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;
using VintageTimepieceService.IService;

namespace VintageTimepieceService.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<APIResponse<Role>> CreateNewRole(Role role)
        {
            var existsRole = _roleRepository.FindAll().Where(x => x.RoleName.ToLower().Equals(role.RoleName.ToLower()) && x.IsDel == false).FirstOrDefault();
            if (existsRole != null)
            {
                return new APIResponse<Role>
                {
                    Message = "Role is exists",
                    isSuccess = false,
                };
            }
            var result = await Task.FromResult(_roleRepository.CreateNewRole(role));
            return new APIResponse<Role>
            {
                Message = "Create role sucess",
                isSuccess = true,
                Data = result
            };

        }

        public async Task<APIResponse<Role>> DeleteRole(int id)
        {
            var existsRole = _roleRepository.GetRoleById(id);
            if (existsRole == null)
            {
                return new APIResponse<Role>
                {
                    Message = "Role not exists",
                    isSuccess = false,
                };
            }
            var result = await Task.FromResult(_roleRepository.DeleteRole(existsRole));
            if (result == null)
                return new APIResponse<Role>
                {
                    Message = "Delete role fail",
                    isSuccess = false,
                };
            return new APIResponse<Role>
            {
                Message = "Delete role success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<List<Role>>> GetAllRole()
        {
            var result = await Task.FromResult(_roleRepository.GetAllRole());
            return new APIResponse<List<Role>>
            {
                Message = "Get all role success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<Role>> GetRoleById(int id)
        {
            var result = await Task.FromResult(_roleRepository.GetRoleById(id));
            if (result == null)
                return new APIResponse<Role>
                {
                    Message = "Role not exists",
                    isSuccess = false,
                };

            return new APIResponse<Role>
            {
                Message = "Finded the role",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<Role>> GetRoleByName(string name)
        {
            var result = await Task.FromResult(_roleRepository.GetRoleByName(name));
            if (result == null)
                return new APIResponse<Role>
                {
                    Message = "Role not exists",
                    isSuccess = false,
                };
            return new APIResponse<Role>
            {
                Message = "Finded the role",
                isSuccess = true,
                Data = result
            };
        }
    }
}
