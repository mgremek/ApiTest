using Api_Cats.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api_Cats.Functions.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
        {
            private readonly CatsDbContext _context;

            public GetAllProductsQueryHandler(CatsDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancelationToken)
            {
                var bookList = await _context.Products.ToListAsync();

                if (bookList == null)
                {
                    return null;
                }

                return bookList.AsReadOnly();
            }
        }
    }
}
