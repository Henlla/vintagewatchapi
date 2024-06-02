using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageTimePieceRepository.IRepository
{
    public interface IHashPasswordRepository
    {
        public string Hash(string password);
        public bool VerifyPassword(string HashPassword, string password);
    }
}
