using System.Data.Entity;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(VintagedbContext context) : base(context)
        {
        }

        public Role? CreateNewRole(Role role)
        {
            return Add(role);
        }

        public Role? DeleteRole(Role role)
        {
            return Update(role);
        }

        public List<Role> GetAllRole()
        {
            return FindAll().Where(role => role.IsDel == false).ToList();
        }

        public Role? GetRoleById(int id)
        {
            return FindAll().Where(r => r.RoleId == id).SingleOrDefault();
        }

        public Role? GetRoleByName(string roleName)
        {
            var role = FindAll().Where(r => r.IsDel == false && r.RoleName.Equals(roleName)).SingleOrDefault();
            return role;
        }
    }
}
