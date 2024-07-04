
namespace VintageTimepieceModel.Models.Shared
{
    public class APIResponse<T>
    {
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
        public bool isSuccess { get; set; }
        public T? Data { get; set; }
    }
}
