using DataAccess.Entities;
using DataAccess.Repositories;

namespace LibraryConsoleApp
{

    internal class Program
    {
        private static AuthorRepository _authorRepository;
        private static BookRepository _bookRepository;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var dbcf = new LibraryDbContextFactory();
            var context = dbcf.CreateDbContext(args);
            _authorRepository = new AuthorRepository(context);
            _bookRepository = new BookRepository(context);

            while (true)
            {
                PrintOptions();
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        await PrintAuthors();
                        break;
                    case "2":

                        break;
                    case "3":
                        await CreateAuthor();
                        break;
                    case "4":

                        break;
                    case "5":

                        break;
                    case "6":
                        Environment.Exit(1);
                        break;
                }
            }
        }
        public static void PrintOptions()
        {
            Console.WriteLine("1. authors list");
            Console.WriteLine("2. show author");
            Console.WriteLine("3. create author");
            Console.WriteLine("4. edit author");
            Console.WriteLine("5. delete author");
            Console.WriteLine("6. System exit");
        }
        public static async Task CreateAuthor()
        {
            var author = new Author();
            await Console.Out.WriteLineAsync("iveskite autoriaus varda");
            author.Name = Console.ReadLine();
            await Console.Out.WriteLineAsync("iveskite autoriaus pavarde");
            author.Surname = Console.ReadLine();
            await _authorRepository.CreateAsync(author);

        }
        public static async Task PrintAuthors()
        {
            var authors = await _authorRepository.readAllAsync();

            Console.WriteLine("Authors:");
            Console.WriteLine($"{"Id",-5} {"Vardas",-20} {"Pavardė",-20} {"parase knygu",-5}");
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.Id,-5} {author.Name,-20} {author.Surname,-20} {author.Books.Count,-20}");
            }
        }
    }
}
