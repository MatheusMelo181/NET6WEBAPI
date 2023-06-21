using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/", () => new {Name = "Matheus", Age = 21});
app.MapGet("/AddHeader", (HttpResponse response) => {
    response.Headers.Add("Teste", "Matheus Melo");
    return new {Name = "Matheus Melo", Age = 21};
});

//Salvar produto
app.MapPost("/products", (Product product) =>{
    ProductRepository.Add(product);
    return Results.Created($"/products/{product.Code}", product.Code);
 });

//Pesquisar pelo código do produto
app.MapGet("/products/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    if(product != null)
        return Results.Ok(product);
    return Results.NotFound();    
});

//Editando produto
app.MapPut("/products", (Product product) =>{
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok();
});

//Deletando o produto
app.MapDelete("/products/{code}", ([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Delete(productSaved);
    return Results.Ok();
});

app.Run();

public static class ProductRepository{
    public static List<Product> Products { get; set; }

    public static void Add(Product product){
        if(Products == null){
            Products = new List<Product>();
            Products.Add(product);
        }
    }

    public static Product GetBy(string code){
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Delete(Product product){
        Products.Remove(product);
    }
}

public class Product{
    public string Code { get; set; }

    public string Name {get; set;}
}