using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AuthorRepository
    {
        private readonly LibraryDbContext _context;
        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;

        }
        public async Task CreateAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }
        public async Task<Author> ReadAsync(long id)
        {
            return await _context.Authors
                  .Select(a => new Author
                  {
                      Id = a.Id,
                      Name = a.Name,
                      Surname = a.Surname,
                      // Only include relevant Book properties (e.g., Id, Title)
                      Books = a.Books.Select(b => new Book { Id = b.Id, Title = b.Title }).ToList()
                  })
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<List<Author>> ReadAllAsync()
        {
            return await _context.Authors
            .Select(a => new Author
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname,
                // Only include relevant Book properties (e.g., Id, Title)
                Books = a.Books.Select(b => new Book { Id = b.Id, Title = b.Title }).ToList()
            })
            .ToListAsync();
        }
        public async Task UpdateAsync(Author author)
        {
            var eAuthor = await _context.Authors.FindAsync(author.Id);
            if (eAuthor == null)
            {
                throw new KeyNotFoundException($"Author with Id {author.Id} not found.");
            }
            _context.Entry(eAuthor).CurrentValues.SetValues(author);
            _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(long id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                throw new KeyNotFoundException($"Author with Id {id} not found.");
            }
            _context.Authors.Remove(author);
            _context.SaveChangesAsync();
        }
    }
}
