using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetName";

    //Static vì dữ liệu chỉ tồn tại 1 bản duy nhất  trong toàn bộ hệ thống
    //readonly vì ngăn ko cho gán lại biến games bằng 1 danh sách khác sau khi tạo
    private static readonly List<GameDto> games = [
    new (
        1,
        "Mario",
        "platformer",
        19.99M,
        new DateOnly(1999, 1, 1) ),
    new (
        2,
        "GTAV",
        "action",
        59.99M,
        new DateOnly(2015, 1, 1) ),
    new (
        3,
        "Street Fighter",
        "fighting",
        29.99M,
        new DateOnly(2001, 1, 1) ),
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        //Mô tả điểm cuối (Endpoint) cho yêu cầu GET 
        //Trả về all games có sẵn in API
        //GET /games
        group.MapGet("/", () => games);
        //app.MapGet("/games", () => games); 


        //Get /games/{Id}
        //app.MapGet("/games/{id}", (int id)
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            if (game is null) return Results.NotFound();
            return Results.Ok(game);
        }).WithName(GetGameEndpointName);
        //app.MapGet("/games/{id}", (int id) => {
        // var game = games.Find(game => game.Id == id);
        //return game is null ? Results.NotFound() : Results.Ok(game);
        // });

        //Post /games
        //app.MapPost("/games", (CreateGameDto newGame)
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);
            //Tham số đầu tiên là tên route để KHang sử dụng để truy cập game vừa tạo
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // PUT /games/{id}
        //app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) 
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);

            if (index == -1) return Results.NotFound();

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        //DELETE /games/{id}
        //app.MapDelete("/games/{id}", (int id)
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(g => g.Id == id);

            return Results.NoContent();
        });
    }
}
