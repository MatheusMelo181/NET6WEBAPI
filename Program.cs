using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name = "Matheus", Age = 21});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "Matheus Melo");
    return new {Name = "Matheus Melo", Age = 21};
});

app.MapPost("/SaveProduct", (Product product) =>{
    return product.Code + " - " + product.Name;
 });

//api.app.com/users?datestart={date}&dateend={date}
app.MapGet("/GetProduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) => {
    return dateStart + " - " + dateEnd;
});

//api.app.com/users/{code}
app.MapGet("/GetProduct/{code}", ([FromRoute] string code) => {
    return code;
});

app.MapGet("/GetProductByHeader", (HttpRequest request) => {
    return request.Headers["product-code"].ToString();
});

app.Run();

public class Product{
    public string Code { get; set; }

    public string Name {get; set;}
}