using System.ComponentModel.DataAnnotations;

namespace GolfViewApartments.API.DTOs
{
    public class ConferencePackageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string IconClass { get; set; } = "";
        public decimal Price { get; set; }
    }
}