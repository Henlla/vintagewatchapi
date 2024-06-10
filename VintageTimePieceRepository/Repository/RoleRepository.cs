using System.Data.Entity;
using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(VintagedbContext context) : base(context)
        {
        }

        public async Task<Role> DeleteRole(int roleId)
        {
            var role = await Task.FromResult(await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId && r.IsDel == false));
            var existsUserHaveRole = await Task.FromResult(await _context.Users.FirstOrDefaultAsync(u => u.RoleId == roleId && u.IsDel == false));
            if (existsUserHaveRole != null)
            {
                return null;
            }
            role.IsDel = true;
            return await Update(role);
        }
    }
}
