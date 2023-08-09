using CoreLibrary.Data;
using CoreLibrary.Models;
using CoreLibrary.Repositories.Interfaces;

namespace CoreLibrary.Repositories
{
    public class BookRepository : BaseCrudRepository<Book>, IBookRepository
    {
        public BookRepository(Context context) : base(context, context.Books) {

        }
    }
}
