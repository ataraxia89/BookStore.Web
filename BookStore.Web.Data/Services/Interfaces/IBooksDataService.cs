using BookStore.Models.Books;
using BookStore.Web.Data.Tools;

namespace BookStore.Web.Data.Services.Interfaces;

public interface IBooksDataService
{
    Task<ApiResult<List<BookDTO>>> GetBooksAsync();

    Task<ApiResult<BookDTO>> GetBookAsync(long id);

    Task<ApiResult<BookDTO>> AddBookAsync(CreateBookDTO createDTO);

    Task<ApiResult<BookDTO>> UpdateBookAsync(long bookId, UpdateBookDTO updateDTO);

    Task<ApiResult> DeleteBookAsync(long id);
}