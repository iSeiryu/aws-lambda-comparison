using Microsoft.AspNetCore.Mvc;

namespace ShinyBff.Controllers;

[Route("api/[controller]")]
public class TodosController : ControllerBase {
    static readonly HttpClient _client = new() {BaseAddress = new Uri("https://jsonplaceholder.typicode.com")};

    [HttpGet("{id}")]
    public async Task<Todo[]> GetBook(int id) {
        var task1 = _client.GetFromJsonAsync<Todo>("/todos/" + id);
        var task2 = _client.GetFromJsonAsync<Todo>("/todos/" + id);
        await Task.WhenAll(task1, task2);

        return new[] {task1.Result, task1.Result};
    }

    public class Todo {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
}