
namespace VintageTimepieceModel.Models.ResponseModel
{
    public class APIResponse
    { 
        public string? Message { get; set; }
        public bool isSuccess { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
