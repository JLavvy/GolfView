namespace GolfViewApartments.API.DTOs
{
    /// <summary>
    /// Represents an apartment returned in availability search results.
    /// Mirrors the Apartment model used on the frontend (SearchResults.razor).
    /// </summary>
    public class AvailableApartmentDto
    {
        // Matches frontend: apartment.Id
        public string Id { get; set; } = string.Empty;

        // Matches frontend: apartment.Name
        public string Name { get; set; } = string.Empty;

        // Matches frontend: apartment.Description
        public string Description { get; set; } = string.Empty;

        // Matches frontend: apartment.Image
        public string Image { get; set; } = string.Empty;

        // Matches frontend: apartment.Bedrooms
        public string Bedrooms { get; set; } = string.Empty;

        // Matches frontend: apartment.MaxGuests
        public int MaxGuests { get; set; }

        // Matches frontend: apartment.Size
        public string Size { get; set; } = string.Empty;

        // Number of rooms still available for the requested dates
        public int AvailableRooms { get; set; }
    }
}