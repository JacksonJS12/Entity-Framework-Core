using AutoMapper;
using ProductShop.Models;

namespace ProductShop;

using Newtonsoft.Json;
using ProductShop.DTOs.Import;

using ProductShop.Data;
public class StartUp
{
    public static void Main()
    {
        ProductShopContext context = new ProductShopContext();
        string productJson = File.ReadAllText(@"../../../Datasets/products.json");

        string result = ImportProducts(context, productJson);
        Console.WriteLine(result);
    }


    public static string ImportUsers(ProductShopContext context, string inputJson)
    {
        IMapper mapper = CreateMapper();

        ImportUserDto[] userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);


        ICollection<User> validUsers = new HashSet<User>();
        foreach (ImportUserDto userDto in userDtos)
        {
            User user = mapper.Map<User>(userDto);

            validUsers.Add(user);
        }

        //Here we have all valid users ready for the DB
        context.Users.AddRange(validUsers);
        context.SaveChanges();

        return $"Successfully imported {validUsers.Count}";
    }

    public static string ImportProducts(ProductShopContext context, string inputJson)
    {
        IMapper mapper = CreateMapper();

        ImportProductDto[] productDtos =
            JsonConvert.DeserializeObject<ImportProductDto[]>(inputJson);

        Product[] products = mapper.Map<Product[]>(productDtos);

        context.Products.AddRange(products);
        context.SaveChanges();

        return $"Successfully imported {products.Length}";
    }

    private static IMapper CreateMapper()
    {
        return new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductShopProfile>();
        }));

    }
}

