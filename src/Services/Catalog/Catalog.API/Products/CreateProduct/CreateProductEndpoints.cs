namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            try
            {

            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                //var result = await sender.Send(request);// it is not throwing any error even this handler accepts CreateProductCommand
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/product/{response.Id}", response);
            }).WithName("CreateProduct")
              .ProducesProblem(StatusCodes.Status400BadRequest); ;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
