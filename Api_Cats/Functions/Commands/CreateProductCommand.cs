using Api_Cats.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Cats.Functions.Commands
{
    public class CreateProductCommand : IRequest<int>
    {      
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string UrlPicture { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly CatsDbContext _context;

            public CreateProductCommandHandler(CatsDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = new Product();

                product.Name=request.Name;
                product.Code=request.Code;
                product.Description=request.Description;
                product.Price=request.Price;
                product.UrlPicture=request.UrlPicture;

                _context.Products.Add(product);

                int result = await _context.SaveChangesAsync();

                return result;
            }
        }
    }
    
}
