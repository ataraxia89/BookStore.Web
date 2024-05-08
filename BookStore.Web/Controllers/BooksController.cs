using BookStore.Models.Books;
using BookStore.Web.Data.Services.Interfaces;
using BookStore.Web.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers;

[Route("[controller]")]
public class BooksController(IBooksDataService booksDataService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        var createDTO = new CreateBookDTO();

        return View(createDTO);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(CreateBookDTO createDTO)
    {
        var result = await booksDataService.AddBookAsync(createDTO);

        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        throw new ApiException(result.StatusCode);
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(long id)
    {
        var result = await booksDataService.GetBookAsync(id);

        var updateDTO = new UpdateBookDTO
        {
            Title = result.Object.Title,
            Author = result.Object.Author,
            PublishDate = result.Object.PublishDate,
            Pages = result.Object.Pages,
            Genre = result.Object.Genre,
            Synopsis = result.Object.Synopsis,
            Rating = result.Object.Rating,
            ISBN = result.Object.ISBN
        };

        return View(updateDTO);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Edit(long id, UpdateBookDTO updateDTO)
    {
        var result = await booksDataService.UpdateBookAsync(id, updateDTO);

        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        throw new ApiException(result.StatusCode);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await booksDataService.DeleteBookAsync(id);

        if (result.IsSuccess)
        {
            return RedirectToAction("Index");
        }

        throw new ApiException(result.StatusCode);
    }
}