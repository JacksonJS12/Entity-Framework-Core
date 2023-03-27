using BookShop.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookShop;
using Data;
using Initializer;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        //DbInitializer.ResetDatabase(db);

        string ageRestrictionInput = Console.ReadLine();
        string result = GetBookByAgeRestriction(dbContext, ageRestrictionInput);

        Console.WriteLine(result);
    }

    public static string GetBookByAgeRestriction(BookShopContext dbContext, string command)
    {
        bool hasParsed = Enum.TryParse(typeof(AgeRestriction), command, true, out object? ageRestrictionObj);
        AgeRestriction ageRestriction;
        if (hasParsed)
        {
            ageRestriction = (AgeRestriction)ageRestrictionObj;

            string[] bookTitles = dbContext.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            return String.Join(Environment.NewLine, bookTitles);
        }

        return null;
    }
}



