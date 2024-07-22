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
            var result = await _roleRepository.CreateNewRole(role);
            return new APIResponse<Role>
            {
                Message = "Create role sucess",
                isSuccess = true,
                Data = result
            };

        }

        public async Task<APIResponse<Role>> DeleteRole(int id)
        {
            var existsRole = await _roleRepository.GetRoleById(id);
            if (existsRole == null)
            {
                return new APIResponse<Role>
                {
                    Message = "Role not exists",
                    isSuccess = false,
                };
            }
            var result = await _roleRepository.DeleteRole(existsRole);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Delete role success" : "Delete role fail";

            return new APIResponse<Role>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<List<Role>>> GetAllRole()
        {
            var result = await _roleRepository.GetAllRole();
            bool isSuccess = false;
            if (result.Count > 0)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Get all role success" : "Don't have any role";

            return new APIResponse<List<Role>>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Role>> GetRoleById(int id)
        {
            var result = await _roleRepository.GetRoleById(id);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Finded the role" : "Role not exists";

            return new APIResponse<Role>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }

        public async Task<APIResponse<Role>> GetRoleByName(string name)
        {
            var result = await _roleRepository.GetRoleByName(name);
            bool isSuccess = false;
            if (result != null)
            {
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            var message = isSuccess ? "Finded the role" : "Role not exists";


            return new APIResponse<Role>
            {
                Message = message,
                isSuccess = isSuccess,
                Data = result
            };
        }
    }
}
