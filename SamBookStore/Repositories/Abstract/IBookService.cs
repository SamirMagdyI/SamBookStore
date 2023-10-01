using SamBookStore.Models.Domain;

namespace SamBookStore.Repositories.Abstract
{
    public interface IBookService
    {
        bool Add(Book model);
        bool Update(Book model);
        bool Delete(int id);
        Book FindById(int id);
        List<Book> Filter(string BookName, int genreId, int authorId, int publisherID);
        IEnumerable<Book> GetAll();
    }
}
