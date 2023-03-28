using System.Diagnostics;
using System.Text;
using BookShop.Models.Enums;

namespace BookShop;
using Data;
using Initializer;

public class StartUp
{
    public static void Main()
    {
        var dbContext = new BookShopContext();
        //DbInitializer.ResetDatabase(dbContext);

        //string input = Console.ReadLine();

        //string result = GetBooksByAgeRestriction(dbContext, input);
        //string result = GetGoldenBooks(dbContext);
        //string result = GetBooksByCategory(dbContext, input);
        // result = GetAuthorNamesEndingIn(dbContext, input);
        //string result = CountCopiesByAuthor(dbContext);
        //string result = GetTotalProfitByCategory(dbContext);
        //string result = GetMostRecentBooks(dbContext);
        IncreasePrices(dbContext);

        //Console.WriteLine(result);
    }

    public static string GetBooksByAgeRestriction(BookShopContext context, string command)
    {
        AgeRestriction ageRestriction;

        var isParseSuccess = Enum.TryParse<AgeRestriction>(command, true, out ageRestriction);

        if (!isParseSuccess)
        {
            return string.Empty;
        }

        string[] bookTitles = context
            .Books
            .Where(b => b.AgeRestriction == ageRestriction)
            .Select(b => b.Title)
            .OrderBy(t => t)
            .ToArray();

        return string.Join(Environment.NewLine, bookTitles);
    }

    public static string GetGoldenBooks(BookShopContext dbContext)
    {
        string[] bookTitles = dbContext.Books
            .Where(b => b.EditionType == EditionType.Gold &&
                        b.Copies < 5000)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .ToArray();

        return String.Join(Environment.NewLine, bookTitles);

    }

    public static string GetBooksByCategory(BookShopContext dbContext, string input)
    {
        string[] categories = input
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(c => c.ToLower())
            .ToArray();

        var bookTitles = dbContext.Books
            .Where(b => b.BookCategories
                .Any(bc => categories.Contains(bc.Category.Name.ToLower())))
            .OrderBy(b => b.Title)
            .Select(b => b.Title)
            .ToArray();

        return String.Join(Environment.NewLine, bookTitles);
    }

    public static string GetAuthorNamesEndingIn(BookShopContext dbContext, string input)
    {
        string[] aurhorNames = dbContext.Authors
            .Where(a => a.FirstName.EndsWith(input))
            .OrderBy(a => a.FirstName)
            .ThenBy(a => a.LastName)
            .Select(a => $"{a.FirstName} {a.LastName}")
            .ToArray();

        return String.Join(Environment.NewLine, aurhorNames);
    }

    public static string CountCopiesByAuthor(BookShopContext dbContext)
    {
        StringBuilder sb = new StringBuilder();

        var authorsWithBookCopies = dbContext.Authors
            .Select(a => new
            {
                FullName = a.FirstName + " " + a.LastName,
                TotalCopies = a.Books.Sum(b => b.Copies)
            })
            .ToArray()
            .OrderByDescending(b => b.TotalCopies);

        foreach (var a in authorsWithBookCopies)
        {
            sb.AppendLine($"{a.FullName} - {a.TotalCopies}");
        }

        return sb.ToString().TrimEnd();
    }

    public static string GetTotalProfitByCategory(BookShopContext dbContext)
    {
        StringBuilder sb = new StringBuilder();
        var categoriesWithProfit = dbContext.Categories
            .Select(c => new
            {
                CategoryName = c.Name,
                TotalProfit = c.CategoryBooks
                    .Sum(cb => cb.Book.Copies * cb.Book.Price)
            })
            .ToArray()
            .OrderByDescending(c => c.TotalProfit)
            .ThenBy(c => c.CategoryName);

        foreach (var c in categoriesWithProfit)
        {
            sb.AppendLine($"{c.CategoryName} ${c.TotalProfit:f2}");
        }

        return sb.ToString().TrimEnd();
    }

    public static string GetMostRecentBooks(BookShopContext dbContext)
    {
        StringBuilder sb = new StringBuilder();

        var categoriesWithMostRecentBooks = dbContext.Categories
            .OrderBy(c => c.Name)
            .Select(c => new
            {
                CategoryName = c.Name,
                MostRecentBooks = c.CategoryBooks
                    .OrderByDescending(cb => cb.Book.ReleaseDate)
                    .Take(3)
                    .Select(cb => new
                    {
                        BookTitle = cb.Book.Title,
                        ReleaseYear = cb.Book.ReleaseDate.Value.Year
                    })
                    .ToArray()
            })
            .ToArray();

        foreach (var c in categoriesWithMostRecentBooks)
        {
            sb.AppendLine($"--{c.CategoryName}");

            foreach (var b in c.MostRecentBooks)
            {
                sb.AppendLine($"{b.BookTitle} ({b.ReleaseYear})");
            }
        }

        return sb.ToString().TrimEnd();
    }

    public static void IncreasePrices(BookShopContext dbContext)
    {
        var booksToIncreasePrice = dbContext.Books
            .Where(b => b.ReleaseDate.HasValue &&
                        b.ReleaseDate.Value.Year < 2010)
            .ToArray();

        foreach (var book in booksToIncreasePrice)
        {
            book.Price += 5;
        }

        dbContext.SaveChanges();
    }
}



