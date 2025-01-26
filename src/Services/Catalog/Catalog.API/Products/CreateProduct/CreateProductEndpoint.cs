namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, List<string> Category, string Description, string Image, decimal Price);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Define HTTP post endpoint for creating a product
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            // Create a command object from the request
            var command = request.Adapt<CreateProductCommand>();

            // Send the command to the command handler
            var result = await sender.Send(command);

            // Return the result
            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new product")
        .WithDescription("Create a new product with the given name, category, description, image, and price.");
    }
}

