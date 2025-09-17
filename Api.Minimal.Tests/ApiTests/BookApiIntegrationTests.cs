using System.Net;
using System.Net.Http.Json;
using Api.Minimal.Books.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.Minimal.Tests.ApiTests
{
    public class BookApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public BookApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllBooks_ReturnsOk()
        {
            var response = await _client.GetAsync("/books");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var books = await response.Content.ReadFromJsonAsync<IEnumerable<Book>>();
            Assert.NotNull(books);
            Assert.NotEmpty(books);
        }

        [Fact]
        public async Task GetBookById_ReturnsOk_WhenBookExists()
        {
            var response = await _client.GetAsync("/books/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var book = await response.Content.ReadFromJsonAsync<Book>();
            Assert.NotNull(book);
            Assert.Equal(1, book.Id);
        }

        [Fact]
        public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
        {
            var response = await _client.GetAsync("/books/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddBook_ReturnsCreated_WhenBookIsValid()
        {
            var newBook = new Book(5, "New Book", "Author", 2023, "Genre");
            var response = await _client.PostAsJsonAsync("/books", newBook);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var createdBook = await response.Content.ReadFromJsonAsync<Book>();
            Assert.NotNull(createdBook);
            Assert.Equal(newBook.Id, createdBook.Id);
        }

        [Fact]
        public async Task AddBook_ReturnsConflict_WhenBookAlreadyExists()
        {
            var existingBook = new Book(1, "To Kill a Mockingbird", "Harper Lee", 1960, "Fiction");
            var response = await _client.PostAsJsonAsync("/books", existingBook);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNoContent_WhenBookIsUpdated()
        {
            var updatedBook = new Book(1, "Updated Title", "Updated Author", 2023, "Updated Genre");
            var response = await _client.PutAsJsonAsync("/books", updatedBook);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            var nonExistentBook = new Book(999, "Nonexistent", "Author", 2023, "Genre");
            var response = await _client.PutAsJsonAsync("/books", nonExistentBook);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNoContent_WhenBookIsDeleted()
        {
            var response = await _client.DeleteAsync("/books/1");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            var response = await _client.DeleteAsync("/books/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
