using System.Net;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

namespace BrightlineBff; 

public class Books {
    public class Book {
        public int id {get;set;}
        public DateTime published {get;set;}
        public string title {get;set;}
        public bool completed {get;set;}
    }

    public async Task<APIGatewayProxyResponse> Add(APIGatewayProxyRequest request,
                                                   ILambdaContext context) {
        var book = JsonSerializer.Deserialize<Book>(request.Body);
        
        book.title += "modified";
        book.title = book.title.ToUpper();
        book.published = book.published.AddDays(1);
        book.completed = !book.completed;
        
        return new APIGatewayProxyResponse {
            StatusCode = (int) HttpStatusCode.OK,
            Body = JsonSerializer.Serialize(book),
            Headers = new Dictionary<string, string> {{"Content-Type", "application/json"}}
        };
    }
}