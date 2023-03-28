using BookShop.Models.Enums;

namespace BookShop;
using Data;
using Initializer;

public class StartUp
{
    public static void Main()
    {
        var dbContext = new BookShopContext();
        //DbInitializer.ResetDatabase(db);

        string input = Console.ReadLine();
        //string result = GetBooksByAgeRestriction(dbContext, input);
        //string result = GetGoldenBooks(dbContext);
        string result = GetBooksByCategory(dbContext, input);

        Console.WriteLine(result);
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

        string[] bookTitles = dbContext.Books
            .Where(b => b.BookCategories
                .Any(bc => categories.Contains(bc.Category.Name.ToLower())))
            .OrderBy(b => b.Title)
            .Select(b => b.Title)
            .ToArray();

        return String.Join(Environment.NewLine, bookTitles);
    }
}



