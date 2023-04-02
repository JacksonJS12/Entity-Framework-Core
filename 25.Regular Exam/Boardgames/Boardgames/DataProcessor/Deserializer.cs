namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ImportCreatorDto[]), new XmlRootAttribute("Creators"));
            var dtos = (ImportCreatorDto[])serializer.Deserialize(new StringReader(xmlString));

            var creators = new List<Creator>();

            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var creator = new Creator
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Boardgames = new List<Boardgame>()
                };

                foreach (var boardgameDto in dto.Boardgames)
                {
                    if (!IsValid(boardgameDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var categoryType = Enum.Parse<CategoryType>(boardgameDto.CategoryType);

                    var boardgame = new Boardgame
                    {
                        Name = boardgameDto.Name,
                        Rating = boardgameDto.Rating,
                        YearPublished = boardgameDto.YearPublished,
                        CategoryType = categoryType,
                        Mechanics = boardgameDto.Mechanics
                    };

                    creator.Boardgames.Add(boardgame);
                }

                creators.Add(creator);

                sb.AppendLine(String.Format(SuccessfullyImportedCreator, creator.FirstName, creator.LastName, creator.Boardgames.Count));
            }

            context.AddRange(creators);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            var dtos = JsonConvert.DeserializeObject<IEnumerable<ImportSellerDto>>(jsonString);

            var validSeller = new List<Seller>();
            var boardgame = context.Boardgames.Select(b => b.Id)
                .ToList();
            foreach (var dto in dtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var seller = new Seller
                {
                    Name = dto.Name,
                    Address = dto.Address,
                    Country = dto.Country,
                    Website = dto.Website
                };

                int boardgamesCount = 0;
                foreach (var boardgameId in dto.Boardgames.Distinct())
                {
                    if (!boardgame.Contains(boardgameId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    BoardgameSeller boardgameSeller = new BoardgameSeller()
                    {
                        Seller = seller,
                        BoardgameId = boardgameId
                    };

                    seller.BoardgamesSellers.Add(boardgameSeller);
                    boardgamesCount++;
                }

                validSeller.Add(seller);

                sb.AppendLine(String.Format(SuccessfullyImportedSeller, seller.Name, boardgamesCount));
            }

            context.Sellers.AddRange(validSeller);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
