namespace DataAccess.Entities
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }

        public ICollection<Book>? Books { get; set; }

    }
}
