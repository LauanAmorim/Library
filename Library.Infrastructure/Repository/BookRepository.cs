using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }
        public async Task<Book> GetByISBN(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
    }
}
