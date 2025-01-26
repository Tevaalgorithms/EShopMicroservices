using BuildingBlocks.CQRS;
using Catalog.API.Models;


namespace Catalog.API.Products.CreateProduct;

    public record CreateProductCommand(string Name, List<string> Category, string Description, string Image, decimal Price):
        ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler 
      : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // create product entity from command object
            var product = new Product{
                Name = command.Name, 
                Category = command.Category,
                Description = command.Description,
                Image = command.Image, 
                Price = command.Price 
            };

            // Save product entity to database

            // return CreateProduct Result 
            return new CreateProductResult(Guid.NewGuid());
        }
    }
    

