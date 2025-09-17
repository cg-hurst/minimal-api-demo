using Api.Minimal.Books.Models;
using Api.Minimal.Books.Services;

namespace Api.Minimal.Tests.ServiceTests;



/// <summary>
/// TESTS FOR BOOK SERVICE
/// Ideally the service would not have hardcoded data but that's just for this simple example
/// </summary>
public class BookServiceTests
{
    [Fact]
    public async Task GetAllBooks_ShouldReturnAllBooks()
    {
        // Arrange
        var service = new BookService(); // Fully qualify if needed

        // Act
        var books = await service.GetAllBooksAsync();

        // Assert
        Assert.NotEmpty(books);
    }

    [Fact]
    public async Task GetBookById_ShouldReturnCorrectBook()
    {
        // Arrange
        var service = new BookService();

        // Act
        var book = await service.GetBookByIdAsync(1);

        // Assert
        Assert.NotNull(book);
        Assert.Equal("To Kill a Mockingbird", book!.Title);
    }

    [Fact]
    public async Task AddBook_ShouldAddBookSuccessfully()
    {
        // Arrange
        var service = new BookService();
        var newBook = new Book(5, "The Lord of the Rings", "J. R. R. Tolkien", 1954, "Fantasy");

        // Act
        var result = await service.AddBookAsync(newBook);

        // Assert
        Assert.True(result);

        var addedBook = await service.GetBookByIdAsync(5);
        Assert.NotNull(addedBook);
    }

    [Fact]
    public async Task UpdateBook_ShouldUpdateBookSuccessfully()
    {
        // Arrange
        var service = new BookService();
        var updatedBook = new Book(1, "To Kill a Mockingbird", "Harper Lee", 1960, "Classic Fiction");

        //Act
        var result = await service.UpdateBookAsync(updatedBook);

        // Assert
        Assert.True(result);
        var book = await service.GetBookByIdAsync(1);
        Assert.NotNull(book);
        Assert.Equal("Classic Fiction", book!.Genre);
    }

    [Fact]
    public async Task DeleteBook_ShouldDeleteBookSuccessfully()
    {
        // Arrange
        var service = new BookService();

        // Act
        var result = await service.DeleteBookAsync(1);

        // Assert
        Assert.True(result);

        var addedBook = await service.GetBookByIdAsync(1);
        Assert.Null(addedBook);
    }

    [Fact]
    public async Task AddBook_ShouldFailForDuplicateId()
    {
        // Arrange
        var service = new BookService();
        var duplicateBook = new Book(1, "Duplicate Book", "Some Author", 2020, "Fiction");

        // Act
        var result = await service.AddBookAsync(duplicateBook);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateBook_ShouldFailForNonExistentBook()
    {
        // Arrange
        var service = new BookService();
        var nonExistentBook = new Book(999, "Non-Existent Book", "Unknown Author", 2020, "Fiction");

        // Act
        var result = await service.UpdateBookAsync(nonExistentBook);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteBook_ShouldFailForNonExistentBook()
    {
        // Arrange
        var service = new BookService();

        // Act
        var result = await service.DeleteBookAsync(999);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetBookById_ShouldReturnNullForNonExistentBook()
    {
        // Arrange
        var service = new BookService();

        // Act
        var book = await service.GetBookByIdAsync(999);

        // Assert
        Assert.Null(book);
    }
}
