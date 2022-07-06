using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BrightlineBff;

public class Todos {
    static readonly HttpClient _client = new() {BaseAddress = new Uri("https://jsonplaceholder.typicode.com")};

    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The API Gateway response.</returns>
    public async Task<APIGatewayProxyResponse> Get(APIGatewayProxyRequest request,
                                                   ILambdaContext context) {
        context.Logger.LogInformation("Get Request\n");

        var id = request.QueryStringParameters["id"];

        var task1 = _client.GetFromJsonAsync<Todo>("/todos/" + id);
        var task2 = _client.GetFromJsonAsync<Todo>("/todos/" + id);
        await Task.WhenAll(task1, task2);

        var body = new[] {task1.Result, task1.Result};

        return new APIGatewayProxyResponse {
            StatusCode = (int) HttpStatusCode.OK,
            Body = JsonSerializer.Serialize(body),
            Headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
        };
    }

    public class Todo {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
}