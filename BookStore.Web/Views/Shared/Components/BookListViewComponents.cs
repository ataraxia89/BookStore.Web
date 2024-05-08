using BookStore.Web.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Views.Shared.Components;

public class BookListViewComponent(IBooksDataService booksDataService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var books = await booksDataService.GetBooksAsync();

        if (!books.IsSuccess)
        {
            return View("Error");
        }

        return View(books.Object);
    }
}