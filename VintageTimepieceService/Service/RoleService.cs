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
            var existsRole = await _roleRepository.FindAll().Where(x => x.RoleName.ToLower().Equals(role.RoleName.ToLower()) && x.IsDel == false).FirstOrDefaultAsync();
            if (existsRole != null)
            {
                return new APIResponse<Role>
                {
                    Message = "Role is exists",
                    isSuccess = false,
                };
            }
            var result = await _roleRepository.Add(role);
            return new APIResponse<Role>
            {
                Message = "Create role sucess",
                isSuccess = true,
                Data = result
            };

        }

        public async Task<APIResponse<Role>> DeleteRole(int id)
        {
            var result = await _roleRepository.DeleteRole(id);
            if (result != null)
                return new APIResponse<Role>
                {
                    Message = "Delete role success",
                    isSuccess = true,
                    Data = result
                };
            return new APIResponse<Role>
            {
                Message = "Delete role fail",
                isSuccess = false,
            };

        }

        public async Task<APIResponse<List<Role>>> GetAllRole()
        {
            var result = await Task.FromResult(_roleRepository.FindAll().Where(r => r.IsDel == false).ToList());
            return new APIResponse<List<Role>>
            {
                Message = "Get all role success",
                isSuccess = true,
                Data = result
            };
        }

        public async Task<APIResponse<Role>> GetRoleByName(string name)
        {
            var result = await Task.FromResult(await _roleRepository.FindAll().Where(r => r.RoleName.Equals(name) && r.IsDel == false).FirstOrDefaultAsync());
            if (result == null)
            {
                return new APIResponse<Role>
                {
                    Message = "Role not exists",
                    isSuccess = false,
                };
            }
            return new APIResponse<Role>
            {
                Message = "Finded the role",
                isSuccess = true,
                Data = result
            };
        }
    }
}
