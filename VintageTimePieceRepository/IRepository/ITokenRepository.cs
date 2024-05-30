using System.IdentityModel.Tokens.Jwt;
using VintageTimepieceModel.Models;

namespace VintageTimePieceRepository.IRepository
{
    public interface ITokenRepository
    {
        public string GenerateJwtToken(User user);
        public RefreshToken GenerateRefreshToken();
    }
}
