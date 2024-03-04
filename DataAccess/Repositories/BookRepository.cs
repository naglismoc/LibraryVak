using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;

        }
        public async Task CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task<Book> readAsync(long id)
        {
            return await _context.Books
                 .Select(b => new Book
                 {
                     Id = b.Id,
                     Title = b.Title,
                     Genre = b.Genre,
                     // Include other desired Book properties
                     Author = new Author // Include the full Author object
                     {
                         Id = b.Author.Id,
                         Name = b.Author.Name,
                         Surname = b.Author.Surname
                     }
                 })
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<List<Book>> readAllAsync()
        {
            return await _context.Books
         .Select(b => new Book
         {
             Id = b.Id,
             Title = b.Title,
             Genre = b.Genre,
             // Include other desired Book properties
             Author = new Author // Include the full Author object
             {
                 Id = b.Author.Id,
                 Name = b.Author.Name,
                 Surname = b.Author.Surname
             }
         })
         .ToListAsync();
        }
        public async Task UpdateAsync(Book book)
        {
            var eBook = await _context.Books.FindAsync(book.Id);
            if (eBook == null)
            {
                throw new KeyNotFoundException($"Book with Id {book.Id} not found.");
            }
            _context.Entry(eBook).CurrentValues.SetValues(book);
            _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(long id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with Id {id} not found.");
            }
            _context.Books.Remove(book);
            _context.SaveChangesAsync();
        }
    }
}
