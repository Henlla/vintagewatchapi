using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VintageTimepieceModel.Models;
using VintageTimepieceModel.Models.Shared;

namespace VintageTimePieceRepository.IRepository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        public Task<Role> DeleteRole(int roleId);
    }
}
