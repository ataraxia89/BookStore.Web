using BookStore.Models.Books;
using BookStore.Web.Data.Services.Interfaces;
using BookStore.Web.Data.Tools;

namespace BookStore.Web.Data.Services;

public class BooksDataService : IBooksDataService
{
    public async Task<ApiResult<List<BookDTO>>> GetBooksAsync()
    {
        var response = await JsonRequestHelper.GetResponse(
            HttpMethod.Get,
            "books");

        return await ApiResult<List<BookDTO>>.GenerateApiResultAsync(response);
    }

    public async Task<ApiResult<BookDTO>> GetBookAsync(long id)
    {
        var response = await JsonRequestHelper.GetResponse(
            HttpMethod.Get,
            ["books", id.ToString()]);

        return await ApiResult<BookDTO>.GenerateApiResultAsync(response);
    }

    public async Task<ApiResult<BookDTO>> AddBookAsync(CreateBookDTO createDTO)
    {
        var response = await JsonRequestHelper.GetResponse(
            HttpMethod.Post,
            "books",
            createDTO);

        return await ApiResult<BookDTO>.GenerateApiResultAsync(response);
    }

    public async Task<ApiResult<BookDTO>> UpdateBookAsync(long bookId, UpdateBookDTO updateDTO)
    {
        var response = await JsonRequestHelper.GetResponse(
            HttpMethod.Put,
            ["books", bookId.ToString()],
            updateDTO);

        return await ApiResult<BookDTO>.GenerateApiResultAsync(response);
    }

    public async Task<ApiResult> DeleteBookAsync(long id)
    {
        var response = await JsonRequestHelper.GetResponse(
            HttpMethod.Delete,
            ["books", id.ToString()]);

        return ApiResult.GenerateApiResult(response);
    }
}