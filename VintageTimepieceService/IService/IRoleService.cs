using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimepieceService.IService
{
    public interface IRoleService
    {
        public Task<APIResponse<List<Role>>> GetAllRole();
        public Task<APIResponse<Role>> CreateNewRole(Role role);
        public Task<APIResponse<Role>> GetRoleByName(string name);
        public Task<APIResponse<Role>> DeleteRole(int id);
        public Task<APIResponse<Role>> GetRoleById(int id);
    }
}
