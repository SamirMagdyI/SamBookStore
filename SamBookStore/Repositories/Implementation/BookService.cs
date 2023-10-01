using Microsoft.EntityFrameworkCore;
using SamBookStore.Models.Domain;
using SamBookStore.Repositories.Abstract;

namespace SamBookStore.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext context;

        public BookService(DatabaseContext context)
        {
            this.context = context;
        }

        public bool Add(Book model)
        {
            try
            {
                context.Book.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.Book.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Book> Filter(string bookName, int genreId, int authorId, int publisherId)
        {

            var books = context.Book.AsQueryable();
            if (bookName != null)
                books = books.Where(b => b.Title.Contains(bookName));
            if (genreId != 0)
                books = books.Where(b => b.GenreId == genreId);
            if (authorId != 0)
                books = books.Where(b => b.AuthorId == authorId);
            if (publisherId != 0)
                books = books.Where(b => b.PubhlisherId == publisherId);
            return books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Genre).ToList();
        }

                public Book FindById(int id)
                {
                    return context.Book.Find(id);
                }

                public IEnumerable<Book> GetAll()
                {
                    var data = context.Book.Include(b => b.Author)
                                            .Include(b => b.Publisher)
                                            .Include(b => b.Genre).ToList();
            return data;
        }

                public bool Update(Book model)
                {
                    try
                    {
                        context.Book.Update(model);
                        context.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
           
        } 
    
