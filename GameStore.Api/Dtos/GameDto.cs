namespace GameStore.Api.Dtos;



// public record GameDto
// {
//     public int Id {get; set; }
//     public required string Name {get; set; }
//     public required string Genre {get; set; }
//     public decimal Price {get; set; }
//     public DateOnly ReleaseDate {get; set; }
// }


//Ngắn gọn hơn:
public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);