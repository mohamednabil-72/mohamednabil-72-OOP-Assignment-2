namespace LibrarySystem
{
    public class Book : LibraryItem
    {
        public Book(string catalogNumber, string title, decimal baseLateFee)
            : base(catalogNumber, title, 21, baseLateFee, 1m)
        {
        }
    }

    public class Dvd : LibraryItem
    {
        public Dvd(string catalogNumber, string title, decimal baseLateFee)
            : base(catalogNumber, title, 7, baseLateFee, 2m)
        {
        }
    }

    public class Magazine : LibraryItem
    {
        public Magazine(string catalogNumber, string title, decimal baseLateFee)
            : base(catalogNumber, title, 3, baseLateFee, 0.5m)
        {
        }
    }
}
