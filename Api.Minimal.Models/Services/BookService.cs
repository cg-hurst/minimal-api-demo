namespace Api.Minimal.Books.Services;

public class BookService
{
    private readonly Dictionary<int, Models.Book> _books = new()
    {
        {1, new Models.Book(1, "To Kill a Mockingbird", "Harper Lee", 1960, "Fiction") },
        {2, new Models.Book(2, "1984", "George Orwell", 1949, "Dystopian") },
        {3, new Models.Book(3, "The Great Gatsby", "F. Scott Fitzgerald", 1925, "Classic") },
        {4, new Models.Book(4, "The Catcher in the Rye", "J.D. Salinger", 1951, "Fiction") }
    };

    public Task<IEnumerable<Models.Book>> GetAllBooksAsync()
    {
        return Task.FromResult(_books.Values.AsEnumerable());
    }

    public Task<Models.Book?> GetBookByIdAsync(int id)
    {
        _books.TryGetValue(id, out var book);
        return Task.FromResult(book);
    }

    public Task<bool> AddBookAsync(Models.Book book)
    {
        if (_books.ContainsKey(book.Id))
        {
            return Task.FromResult(false);
        }

        _books[book.Id] = book;
        return Task.FromResult(true);
    }

    public Task<bool> UpdateBookAsync(Models.Book book)
    {
        if (!_books.ContainsKey(book.Id))
        {
            return Task.FromResult(false);
        }

        _books[book.Id] = book;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteBookAsync(int id)
    {
        return Task.FromResult(_books.Remove(id));
    }
}
