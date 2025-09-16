using Api.Minimal.Books.Models;
using Api.Minimal.Books.Services;
using FluentAssertions;

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
        books.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetBookById_ShouldReturnCorrectBook()
    {
        // Arrange
        var service = new BookService();

        // Act
        var book = await service.GetBookByIdAsync(1);

        // Assert
        book.Should().NotBeNull();
        book!.Title.Should().Be("To Kill a Mockingbird");
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
        result.Should().BeTrue();

        var addedBook = await service.GetBookByIdAsync(5);
        addedBook.Should().NotBeNull();
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
        result.Should().BeTrue();
        var book = await service.GetBookByIdAsync(1);
        book.Should().NotBeNull();
        book!.Genre.Should().Be("Classic Fiction");
    }

    [Fact]
    public async Task DeleteBook_ShouldDeleteBookSuccessfully()
    {
        // Arrange
        var service = new BookService();

        // Act
        var result = await service.DeleteBookAsync(1);

        // Assert
        result.Should().BeTrue();

        var addedBook = await service.GetBookByIdAsync(1);
        addedBook.Should().BeNull();
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
        result.Should().BeFalse();
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
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteBook_ShouldFailForNonExistentBook()
    {
        // Arrange
        var service = new BookService();

        // Act
        var result = await service.DeleteBookAsync(999);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetBookById_ShouldReturnNullForNonExistentBook()
    {
        // Arrange
        var service = new BookService();

        // Act
        var book = await service.GetBookByIdAsync(999);

        // Assert
        book.Should().BeNull();
    }
}
