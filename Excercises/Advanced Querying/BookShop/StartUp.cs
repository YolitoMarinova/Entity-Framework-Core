namespace BookShop
{
    using Data;
    using System;
    using Initializer;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);

                //string input = Console.ReadLine();

                //int input = int.Parse(Console.ReadLine());

                int result = RemoveBooks(db);

                Console.WriteLine(result);
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder result = new StringBuilder();


            var books = context
               .Books
               .Where(b => b.AgeRestriction.ToString().ToLower() == command.ToLower())
               .Select(b => new
               {
                   b.Title
               })
               .OrderBy(b => b.Title)
               .ToList();

            foreach (var book in books)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var goldenBooks = context
                .Books
                .Where(b => b.EditionType.ToString() == "Gold" && b.Copies < 5000)
                .Select(b => new
                {
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in goldenBooks)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            decimal minimalPrice = 40m;

            var books = context
                .Books
                .Where(b => b.Price > minimalPrice)
                .Select(b => new
                {
                    b.Title,
                    b.Price
                })
                .OrderByDescending(b => b.Price)
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return result.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder result = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.BookId,
                    b.Title
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder result = new StringBuilder();

            string[] categories = input
                .ToLower()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var books = context
                .BooksCategories
                .Where(bc => categories.Contains(bc.Category.Name.ToLower()))
                .Select(bc => new
                {
                    bc.Book.Title
                })
                .OrderBy(bc => bc.Title)
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine(book.Title);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder result = new StringBuilder();

            DateTime inputDate = DateTime.Parse(date, new CultureInfo("en-IN"));

            var books = context
                .Books
                .Where(b => b.ReleaseDate < inputDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price
                })
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} - {book.EditionType.ToString()} - ${book.Price:F2}");
            }

            return result.ToString().TrimEnd();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder result = new StringBuilder();

            var authors = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(a => a.FullName)
                .ToList();

            foreach (var author in authors)
            {
                result.AppendLine(author.FullName);
            }

            return result.ToString().TrimEnd();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder result = new StringBuilder();

            var bookTitles = context
                .Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            foreach (var title in bookTitles)
            {
                result.AppendLine(title);
            }


            return result.ToString().TrimEnd();
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder result = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.Author.LastName.ToLower()
                    .StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    AuthorFullName = b.Author.FirstName + " " + b.Author.LastName
                })
                .ToList();

            foreach (var book in books)
            {
                result.AppendLine($"{book.Title} ({book.AuthorFullName})");
            }

            return result.ToString().TrimEnd();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int countOfBooks = context
                .Books
                .Where(b => b.Title.Length > lengthCheck)
                .ToList()
                .Count();

            return countOfBooks;
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var authors = context
                .Authors
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                    TotalCopies = a.Books
                             .Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.TotalCopies)
                .ToList();

            foreach (var author in authors)
            {
                result.AppendLine($"{author.FullName} - {author.TotalCopies}");
            }

            return result.ToString().TrimEnd();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var categories = context
                .Categories
                .Select(c => new
                {
                    c.Name,
                    TotalProfit = c.CategoryBooks.Sum(b => b.Book.Copies * b.Book.Price)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var category in categories)
            {
                result.AppendLine($"{category.Name} ${category.TotalProfit}");
            }

            return result.ToString().TrimEnd();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder result = new StringBuilder();

            var categoriesAndBooks = context
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    RecentBooks = c.CategoryBooks
                                    .OrderByDescending(b => b.Book.ReleaseDate)
                                    .Take(3)
                                    .Select(b => new
                                    {
                                        BookTitle = b.Book.Title,
                                        RealeseYear = b.Book.ReleaseDate.Value.Year
                                    })
                                    .ToList()
                })
                .ToDictionary(c => c.CategoryName, b => b.RecentBooks);

            foreach (var category in categoriesAndBooks)
            {
                result.AppendLine($"--{category.Key}");

                foreach (var book in category.Value)
                {
                    result.AppendLine($"{book.BookTitle} ({book.RealeseYear})");
                }
            }

            return result.ToString().TrimEnd();
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context
                    .Books
                    .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context
                    .Books
                    .Where(b => b.Copies < 4200);

            int counter = 0;

            foreach (var book in books)
            {
                context.Remove(book);

                counter++;
            }

            context.SaveChanges();

            return counter;
        }
    }
}
