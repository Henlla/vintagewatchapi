using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        public Role? DeleteRole(Role role);
        public List<Role> GetAllRole();
        public Role? GetRoleByName(string roleName);
        public Role? CreateNewRole(Role role);
        public Role? GetRoleById(int id);

    }
}
