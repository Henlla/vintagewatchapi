using Microsoft.EntityFrameworkCore;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(VintagedbContext context) : base(context)
        {
        }

        public async Task<Role?> CreateNewRole(Role role)
        {
            return await Add(role);
        }

        public async Task<Role?> DeleteRole(Role role)
        {
            return await Update(role);
        }

        public async Task<List<Role>> GetAllRole()
        {
            return await FindAll().Where(role => role.IsDel == false).ToListAsync();
        }

        public async Task<Role?> GetRoleById(int id)
        {
            return await FindAll().Where(r => r.RoleId == id && r.IsDel == false).SingleOrDefaultAsync();
        }

        public async Task<Role?> GetRoleByName(string roleName)
        {
            var role = await FindAll().Where(r => r.IsDel == false && r.RoleName.Equals(roleName)).SingleOrDefaultAsync();
            return role;
        }
    }
}
