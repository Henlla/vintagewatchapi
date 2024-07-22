using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        public Task<Role?> DeleteRole(Role role);
        public Task<List<Role>> GetAllRole();
        public Task<Role?> GetRoleByName(string roleName);
        public Task<Role?> CreateNewRole(Role role);
        public Task<Role?> GetRoleById(int id);

    }
}
