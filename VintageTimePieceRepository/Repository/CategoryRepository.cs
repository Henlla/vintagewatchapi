using VintageTimepieceModel;
using VintageTimepieceModel.Models;
using VintageTimePieceRepository.IRepository;

namespace VintageTimePieceRepository.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(VintagedbContext context) : base(context)
        {
        }
    }
}
