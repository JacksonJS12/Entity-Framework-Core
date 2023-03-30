using AutoMapper;
using ProductShop.Models;

namespace ProductShop;

using Newtonsoft.Json;
using ProductShop.DTOs.Import;

using ProductShop.Data;
public class StartUp
{
    private static IMapper mapper;
    public static void Main()
    {
        mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductShopProfile>();
        }));
    }


    public static string ImportUsers(ProductShopContext context, string inputJson)
    {
        var userDtos = JsonConvert.DeserializeObject<ImportUserDto[]>(inputJson);

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
}

