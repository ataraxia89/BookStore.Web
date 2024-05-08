# BookStore.Web
## Notes
- The API URL is stored in `BookStore.Web.Data.AppData.cs` (shouldn't need to change)
- The tools and services within the `BookStore.Web.Data` project have been taken and adapted from a home project of mine
- The BookStore API will need to be running first before launching the web application
- Please see notes on [BookStore.API](https://github.com/ataraxia89/BookStore.API?tab=readme-ov-file#notes) on initial data, on first run there will be no books in the database unless the seed data endpoint is called on the API
- The `BookStore.Models` project has been duplicated between solutions, but in a live environment this would be a Nuget package shared between them
