using Api.Minimal.Books.Models;
using Api.Minimal.Books.Services;

namespace Api.Minimal.Api;

public static class AddBookApiExtension
{
    public static WebApplication AddBookApi(this WebApplication app)
    {
        app
            .MapGet("/books", async (BookService service) =>
            {
                var books = await service.GetAllBooksAsync();
                return Results.Ok(books);
            })
            .WithName("GetAllBooks");

        app
            .MapGet("/books/{id:int}", async (int id, BookService service) =>
            {
                var book = await service.GetBookByIdAsync(id);
                return book is not null ? Results.Ok(book) : Results.NotFound();
            })
            .WithName("GetBookById");

        app
            .MapPost("/books", async (Book book, BookService service) =>
            {
                var added = await service.AddBookAsync(book);
                return added ? Results.Created($"/books/{book.Id}", book) : Results.Conflict("A book with the same ID already exists.");
            })
            .WithName("AddBook");

        app
            .MapPut("/books", async (Book updatedBook, BookService service) =>
            {
                var updated = await service.UpdateBookAsync(updatedBook);
                return updated ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateBook");

        app
            .MapDelete("/books/{id:int}", async (int id, BookService service) =>
            {
                var deleted = await service.DeleteBookAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteBook");

        return app;
    }
}
