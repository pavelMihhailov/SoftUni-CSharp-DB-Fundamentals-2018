using System;
using System.Globalization;
using System.Linq;
using System.Text;
using BookShop.Models;
using Microsoft.EntityFrameworkCore.Extensions.Internal;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(x => x.Copies < 4200).ToArray();
            var result = books.Length;

            context.RemoveRange(books);
            context.SaveChanges();

            return result;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(x => x.ReleaseDate.Value.Year < 2010).ToArray();
            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories.OrderBy(c => c.Name).Select(
                c => new
                {
                    c.Name,
                    Books = c.CategoryBooks.Select(cb => cb.Book).OrderByDescending(x => x.ReleaseDate).Take(3)
                }).ToArray();

            var builder = new StringBuilder();

            foreach (var category in categories)
            {
                builder.AppendLine($"--{category.Name}");

                foreach (var book in category.Books)
                {
                    var year = book.ReleaseDate == null ? "no release date" : book.ReleaseDate.Value.Year.ToString();

                    builder.AppendLine($"{book.Title} ({year})");
                }
            }

            return builder.ToString();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)).ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)}").ToArray();

            return string.Join(Environment.NewLine, categories);
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors.OrderByDescending(a => a.Books.Sum(x => x.Copies))
                .Select(a => $"{a.FirstName} {a.LastName} - {a.Books.Sum(x => x.Copies)}");

            return string.Join(Environment.NewLine, authors);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var numberOfBooks = context.Books.Count(x => x.Title.Length > lengthCheck);

            return numberOfBooks;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books.OrderBy(x => x.BookId)
                .Where(x => x.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})").ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var titles = context.Books.OrderBy(x => x.Title)
                .Where(a => a.Title.ToLower().Contains(input.ToLower()))
                .Select(x => x.Title).ToArray();

            return string.Join(Environment.NewLine, titles);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors.OrderBy(a => a.FirstName).ThenBy(a => a.LastName)
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}").ToArray();

            return string.Join(Environment.NewLine, authors);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var inputDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books.OrderByDescending(x => x.ReleaseDate).Where(x => x.ReleaseDate < inputDate)
                .Select(x => $"{x.Title} - {x.EditionType.ToString()} - ${x.Price:f2}").ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.ToLower().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var titles = context.Books
                .Where(x => x.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(x => x.Title).OrderBy(x => x).ToArray();

            return string.Join(Environment.NewLine, titles);
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var titles = context.Books.OrderBy(x => x.BookId)
                .Where(x => x.ReleaseDate.Value.Year != year)
                .Select(x => x.Title).ToArray();

            return string.Join(Environment.NewLine, titles);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var titlesAndPrices = context.Books.OrderByDescending(x => x.Price)
                .Where(x => x.Price > 40)
                .Select(x => $"{x.Title} - ${x.Price:f2}").ToArray();

            var result = string.Join(Environment.NewLine, titlesAndPrices);

            return result;
        }


        public static string GetGoldenBooks(BookShopContext context)
        {
            var titles = context.Books.OrderBy(x => x.BookId)
                .Where(x => (int)x.EditionType == 2 && x.Copies < 5000)
                .Select(x => x.Title).ToArray();

            return string.Join(Environment.NewLine, titles);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            int ageRestrictionValue = -1;

            switch (command.ToLower())
            {
                case "minor":
                    ageRestrictionValue = 0;
                    break;
                case "teen":
                    ageRestrictionValue = 1;
                    break;
                case "adult":
                    ageRestrictionValue = 2;
                    break;
            }

            var titles = context.Books
                .OrderBy(x => x.Title)
                .Where(x => x.AgeRestriction.Equals((AgeRestriction)ageRestrictionValue))
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToArray();

            string result = string.Join(Environment.NewLine, titles);

            return result;
        }
    }
}
