namespace SamBookStore.Models.Domain.ViewModels
{
    public class GetALLViewModel
    {
       public List<Book> Books { get; set; }
       public List<Genre> Genres { get; set; }
       public List<Author> Authors { get; set; }
       public List<Publisher> Publishers { get; set; }

        public string BookNameSearch { get; set; }
        public int SelectedGenreId { get; set; }
        public int SelectedAuthorId { get; set; }
        public int SelectedPublisherId { get; set; }
    }
}
