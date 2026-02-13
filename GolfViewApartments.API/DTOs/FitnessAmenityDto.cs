using System.ComponentModel.DataAnnotations;

namespace GolfViewApartments.API.DTOs
{
public class FitnessAmenityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string IconClass { get; set; } = "";
        public decimal DayRate { get; set; }
        public decimal MonthlyRate { get; set; }
    }
}