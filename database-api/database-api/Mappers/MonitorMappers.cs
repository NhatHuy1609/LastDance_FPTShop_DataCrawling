using database_api.Dtos.Monitor;

namespace database_api.Mappers
{
    public static class MonitorMappers
    {
        public static Entities.Monitor ToEntityFromCreateMonitorRequest(this CreateMonitorRequest request)
        {
            return new Entities.Monitor
            {
                Name = request.Name,
                Url = request.Url,
                ImageUrl = request.ImageUrl,
                Price = request.Price,
                PriceDiscount = request.PriceDiscount,
                Category = "Monitor",
                isAvailable = "true"
            };
        }

        public static MonitorDto ToDto(this Entities.Monitor entity)
        {
            return new MonitorDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Url = entity.Url,
                ImageUrl = entity.ImageUrl,
                Price = entity.Price ?? 0,
                PriceDiscount = entity.PriceDiscount ?? 0,
            };
        }
    }
}