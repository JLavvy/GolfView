// using System.ComponentModel.DataAnnotations;

// namespace GolfViewApartments.API.DTOs.Room
// {
//     /// <summary>
//     /// Response DTO for room information
//     /// </summary>
//     public class RoomResponseDto
//     {
//         public int Id { get; set; }
//         public string Number { get; set; } = string.Empty;
//         public int ApartmentId { get; set; }
//         public string ApartmentName { get; set; } = string.Empty;
//         public string Type { get; set; } = string.Empty;
//         public int Floor { get; set; }
//         public bool IsAvailable { get; set; }
//     }

//     /// <summary>
//     /// Request DTO for updating room availability
//     /// </summary>
//     public class UpdateRoomAvailabilityDto
//     {
//         [Required]
//         public bool IsAvailable { get; set; }
//     }

//     /// <summary>
//     /// Request DTO for bulk room updates
//     /// </summary>
//     public class BulkRoomUpdateDto
//     {
//         [Required]
//         [MinLength(1, ErrorMessage = "At least one room update is required")]
//         public List<RoomUpdateItemDto> Updates { get; set; } = new();
//     }

//     public class RoomUpdateItemDto
//     {
//         [Required]
//         public int Id { get; set; }

//         [Required]
//         public bool IsAvailable { get; set; }
//     }

//     /// <summary>
//     /// Room availability check result
//     /// </summary>
//     public class RoomAvailabilityResponseDto
//     {
//         public int Id { get; set; }
//         public string Number { get; set; } = string.Empty;
//         public string Type { get; set; } = string.Empty;
//         public int Floor { get; set; }
//         public bool IsAvailable { get; set; }
//         public bool IsBookedForDates { get; set; }
//     }
// }