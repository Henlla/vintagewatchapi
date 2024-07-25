
namespace VintageTimepieceModel.Models.Shared
{
    public class APIResponse<T>
    {
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
        public bool isSuccess { get; set; }
        public T? Data { get; set; }
        public int? TotalCount { get; set; }
        public int? CurrentPage { get; set; }
        public int? TotalPages { get; set; }
        public int? PageSize { get; set; }
    }
}
