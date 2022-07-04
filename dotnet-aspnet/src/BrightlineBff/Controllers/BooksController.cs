using Microsoft.AspNetCore.Mvc;

namespace BrightlineBff.Controllers;

[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    // POST api/values
    [HttpPost]
    public Book Post([FromBody]Book book) {
        book.title += "modified";
        book.title = book.title.ToUpper();
        book.published = book.published.AddDays(1);
        book.completed = !book.completed;

        return book;
    }

    public class Book {
        public int id {get;set;}
        public DateTime published {get;set;}
        public string title {get;set;}
        public bool completed {get;set;}
    }
}